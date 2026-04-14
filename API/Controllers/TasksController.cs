using System.Security.Claims;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TasksController : ControllerBase
  {
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
      _taskService = taskService;
    }

    private Guid GetUserId()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

      if (string.IsNullOrEmpty(userId))
        throw new UnauthorizedAccessException("Invalid token");

      return Guid.Parse(userId);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskRequest request)
    {
      await _taskService.CreateTaskAsync(GetUserId(), request);
      return Ok(new { message = "Task created" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var task = await _taskService.GetTaskByIdAsync(GetUserId(), id);
      return Ok(task);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var tasks = await _taskService.GetUserTasksAsync(GetUserId());
      return Ok(tasks);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskStatusRequest request)
    {
      await _taskService.UpdateStatusAsync(GetUserId(), id, request);
      return Ok(new { message = "Status updated" });
    }
  }
}