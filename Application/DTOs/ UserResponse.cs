using Domain.Enums;

namespace Application.DTOs
{
  public class  UserResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
  }
}