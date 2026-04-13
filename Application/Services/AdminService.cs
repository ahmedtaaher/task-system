using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
  public class AdminService
  {
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHash _hasher;

    public AdminService(IUserRepository userRepo, IPasswordHash hasher)
    {
      _userRepo = userRepo;
      _hasher = hasher;
    }

    public async Task CreateUserAsync(CreateUserByAdminRequest request)
    {
      var existing = await _userRepo.GetByEmailAsync(request.Email);
      if (existing != null)
        throw new Exception("Email already exists");

      var user = new User(
        request.Name,
        request.Email,
        _hasher.HashPassword(request.Password),
        Role.User
      );

      await _userRepo.AddAsync(user);
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
      var users = await _userRepo.GetAllAsync();

      return users.Select(u => new UserResponse
      {
        Id = u.Id,
        Name = u.Name,
        Email = u.Email,
        Role = u.Role
      }).ToList();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
      var users = await _userRepo.GetAllAsync();
      var user = users.FirstOrDefault(u => u.Id == userId);

      if (user == null)
        throw new Exception("User not found");

      await _userRepo.DeleteAsync(user);
    }
  }
}