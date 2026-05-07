using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.DTOs;
using UserManagementSystem.DTOs.Authentication;
using UserManagementSystem.Services.Interfaces;

namespace UserManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService): ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest request)
    {
        try
        {
            var authResponse = await _authService.RegisterAsync(request);
            return StatusCode(201, authResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(401, e.Message);
        }
    }
    
    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request)
    {
        try
        {
            var authResponse = await _authService.LoginAsync(request);
            return StatusCode(200, authResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(401, e.Message);
        }
    }

    [HttpGet("get")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserByIdAsync([FromQuery]Guid id)
    {
        try
        {
            var user = await _authService.GetUserById(id);
            return StatusCode(200, user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404);
        }
    }
}