using Domain.Entities;

namespace Application.Interfaces
{
  public interface ITaskRepository
  {
    Task AddAsync(TaskItem task);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<List<TaskItem>> GetByUserIdAsync(Guid userId);
    Task<bool> ExistsSameTitleToday(Guid userId, string title);
    Task SaveChangesAsync();
  }
}