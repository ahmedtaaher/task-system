using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
  public class TaskRepository : ITaskRepository
  {
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
      _context = context;
    }
    public async Task AddAsync(TaskItem task)
    {
      await _context.Tasks.AddAsync(task);
    }

    public async Task<bool> ExistsSameTitleToday(Guid userId, string title)
    {
      var today = DateTime.UtcNow.Date;

      return await _context.Tasks.AnyAsync(t =>
        t.UserId == userId &&
        t.Title == title &&
        t.CreatedAt.Date == today
      );
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
      return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TaskItem>> GetByUserIdAsync(Guid userId)
    {
      return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}