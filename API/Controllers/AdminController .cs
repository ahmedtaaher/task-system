using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize(Roles = "Admin")]
  public class AdminController  : ControllerBase
  {
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
      _adminService = adminService;
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(CreateUserByAdminRequest request)
    {
      await _adminService.CreateUserAsync(request);
      return StatusCode(201, new { message = "User created" });
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _adminService.GetAllUsersAsync();
      return Ok(users);
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
      await _adminService.DeleteUserAsync(id);
      return Ok(new { message = "User deleted" });
    }
  }
}