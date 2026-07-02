using ElderCare.Application.DTOs.Admin;
using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ElderCare.Core.Constants;
namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = Roles.Admin)]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        var stats = new AdminStatsDto
        {
            TotalUsers = 124,
            Doctors = 14,
            Patients = 110,
            ActiveSessions = 9,
            ReportsGenerated = 86
        };

        return Ok(stats);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _adminService.GetAllUsersAsync());
    }

    [HttpPut("users/{id}/role")]
    public async Task<IActionResult> UpdateRole(Guid id, UpdateUserRoleDto dto)
    {
        await _adminService.UpdateUserRoleAsync(id, dto.Role);
        return NoContent();
    }

    [HttpPut("users/{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateUserStatusDto dto)
    {
        await _adminService.UpdateUserStatusAsync(id, dto.IsActive);
        return NoContent();
    }
}