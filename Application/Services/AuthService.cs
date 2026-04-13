
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
  public class AuthService
  {
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHash _hasher;
    private readonly IJwtProvider _jwt;

    public AuthService(IUserRepository userRepo, IPasswordHash hasher, IJwtProvider jwt)
    {
      _userRepo = userRepo;
      _hasher = hasher;
      _jwt = jwt;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
      var email = request.Email.Trim().ToLower();
      var existing = await _userRepo.GetByEmailAsync(email);
      if (existing != null)
        throw new Exception("Email already exists");

      var hashedPassword = _hasher.HashPassword(request.Password);

      var user = new User(
        request.Name,
        email,
        hashedPassword,
        Role.User
      );

      await _userRepo.AddAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
      var user = await _userRepo.GetByEmailAsync(request.Email);
      if (user == null)
        throw new Exception("Invalid credentials");

      var isValidPassword = _hasher.VerifyPassword(request.Password, user.PasswordHash);
      if (!isValidPassword)
        throw new Exception("Invalid credentials");

      var token = _jwt.GenerateToken(
        user.Id,
        user.Email,
        user.Role.ToString()
      );

      return new AuthResponse { Token = token };
    }
  }
}