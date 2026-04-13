using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
  public class TaskService
  {
    private readonly ITaskRepository _taskRepo;
    private readonly ICacheService _cache;
    private readonly IBackgroundQueue _queue;

    public TaskService(ITaskRepository taskRepo, ICacheService cache, IBackgroundQueue queue)
    {
      _taskRepo = taskRepo;
      _cache = cache;
      _queue = queue;
    }

    public async Task CreateTaskAsync(Guid userId, CreateTaskRequest request)
    {
      var exists = await _taskRepo.ExistsSameTitleToday(userId, request.Title);
      if (exists)
        throw new Exception("Task with same title already exists today");

      var task = new TaskItem(
        request.Title,
        request.Description,
        request.Priority,
        userId
      );

      await _taskRepo.AddAsync(task);
      await _taskRepo.SaveChangesAsync();
      
      _queue.Enqueue(async token =>
      {
        await Task.Delay(2000, token); // simulate work

        Console.WriteLine($"Task '{task.Title}' processed in background");
      });
    }

    public async Task<TaskResponse> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
      var cacheKey = $"task:{taskId}";

      var cached = await _cache.GetAsync<TaskResponse>(cacheKey);
      if (cached != null)
        return cached;

      var task = await _taskRepo.GetByIdAsync(taskId);
      if (task == null)
        throw new Exception("Task not found");

      if (task.UserId != userId)
        throw new UnauthorizedAccessException("You cannot access this task");

      var response = MapToResponse(task);
      await _cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5));

      return response;
    }

    public async Task<List<TaskResponse>> GetUserTasksAsync(Guid userId)
    {
      var tasks = await _taskRepo.GetByUserIdAsync(userId);

      tasks = tasks
        .OrderByDescending(t => t.Priority)
        .ThenByDescending(t => t.CreatedAt)
        .ToList();

      return tasks.Select(MapToResponse).ToList();
    }

    public async Task UpdateStatusAsync(Guid userId, Guid taskId, UpdateTaskStatusRequest request)
    {
      var task = await _taskRepo.GetByIdAsync(taskId);
      if (task == null)
        throw new Exception("Task not found");

      if (task.UserId != userId)
        throw new UnauthorizedAccessException("You cannot update this task");

      task.UpdateStatus(request.Status);

      await _taskRepo.SaveChangesAsync();
      await _cache.RemoveAsync($"task:{taskId}");
    }

    private TaskResponse MapToResponse(TaskItem task)
    {
      return new TaskResponse
      {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status,
        Priority = task.Priority,
        CreatedAt = task.CreatedAt
      };
    }
  }
}