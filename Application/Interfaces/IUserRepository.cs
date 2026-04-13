using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
  public interface IUserRepository
  {
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task DeleteAsync(User user);
  }
}