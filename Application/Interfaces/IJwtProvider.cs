namespace Application.Interfaces
{
  public interface IJwtProvider
  {
    string GenerateToken(Guid userId, string email, string role);
  }
}