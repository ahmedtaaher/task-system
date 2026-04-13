using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
  public static class DbSeeder
  {
    public static async Task SeedAdminAsync(AppDbContext context)
    {
      if (await context.Users.AnyAsync(u => u.Role == Role.Admin))
        return;

      var admin = new User(
        "Admin",
        "admin@example.com",
        BCrypt.Net.BCrypt.HashPassword("Admin@123"),
        Role.Admin
      );

      await context.Users.AddAsync(admin);
      await context.SaveChangesAsync();
    }
  }
}