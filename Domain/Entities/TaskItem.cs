using Domain.Enums;

namespace Domain.Entities
{
  public class TaskItem
  {
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }
    public int Priority { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    private TaskItem() { } 

    public TaskItem(string title, string description, int priority, Guid userId)
    {
      if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Title cannot be empty");

      Id = Guid.NewGuid();
      Title = title;
      Description = description;
      Priority = priority;
      Status = Status.Pending;
      CreatedAt = DateTime.UtcNow;
      UserId = userId;
    }

    public void UpdateStatus(Status status)
    {
      Status = status;
    }
  }
}