using Domain.Enums;

namespace Domain.Entities
{
  public class User
  {
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<TaskItem> Tasks { get; private set; } = new();

    private User() { } 

    public User(string name, string email, string passwordHash, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeRole(Role newRole)
    {
      Role = newRole;
    }
  }
}