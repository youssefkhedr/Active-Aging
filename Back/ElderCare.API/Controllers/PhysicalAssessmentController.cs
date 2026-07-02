using ElderCare.Application.DTOs;
using ElderCare.Core.Constants;
using ElderCare.Core.Entities;
using ElderCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/assessment")]
[Authorize(Roles = Roles.Patient)]
public class PhysicalAssessmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public PhysicalAssessmentController(AppDbContext context)
    {
        _context = context;
    }

    // ================= ROM =================

    [HttpPost("rom")]
    public async Task<IActionResult> CreateRom(CreateRomAssessmentDto dto)
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var range = dto.MaxAngle - dto.MinAngle;

        var status = range switch
        {
            > 120 => "normal",
            >= 60 => "limited",
            _ => "restricted"
        };

        var assessment = new RomAssessment
        {
            PatientId = patientId,
            JointType = dto.JointType,
            MaxAngle = dto.MaxAngle,
            MinAngle = dto.MinAngle,
            Status = status,
            SessionId = dto.SessionId
        };

        _context.RomAssessments.Add(assessment);
        await _context.SaveChangesAsync();

        return Ok(new { assessment.Status });
    }

    [HttpGet("rom/history")]
    public async Task<IActionResult> RomHistory()
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var history = await _context.RomAssessments
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return Ok(history);
    }

    // ================= BALANCE =================

    [HttpPost("balance")]
    public async Task<IActionResult> CreateBalance(CreateBalanceAssessmentDto dto)
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var assessment = new BalanceAssessment
        {
            PatientId = patientId,
            SwayScore = dto.SwayScore,
            StabilityIndex = dto.StabilityIndex,
            TestType = dto.TestType
        };

        _context.BalanceAssessments.Add(assessment);
        await _context.SaveChangesAsync();

        return Ok("Balance assessment saved");
    }

    [HttpGet("balance/history")]
    public async Task<IActionResult> BalanceHistory()
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var history = await _context.BalanceAssessments
            .Where(b => b.PatientId == patientId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return Ok(history);
    }
}