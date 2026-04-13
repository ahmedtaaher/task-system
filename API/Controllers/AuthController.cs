using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
      await _authService.RegisterAsync(request);
      return StatusCode(201, new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
      var result = await _authService.LoginAsync(request);
      return Ok(result);
    }
  }
}