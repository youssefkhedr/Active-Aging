using ElderCare.Core.DTOs;
using ElderCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto);
        return Ok(result);
    }
    
    /// <summary>
    /// Sync Clerk OAuth user to backend database
    /// </summary>
    /// <remarks>
    /// Call this endpoint after user authenticates via Clerk (Google OAuth).
    /// Returns a backend JWT token for accessing protected endpoints.
    /// </remarks>
    [HttpPost("clerk-sync")]
    public async Task<IActionResult> ClerkSync(ClerkSyncRequestDto dto)
    {
        var result = await _authService.ClerkSyncAsync(dto);
        return Ok(result);
    }
}