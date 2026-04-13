using Domain.Enums;

namespace Application.DTOs
{
  public class TaskResponse
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}