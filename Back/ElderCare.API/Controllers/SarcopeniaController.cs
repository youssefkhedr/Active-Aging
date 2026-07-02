using ElderCare.Application.DTOs;
using ElderCare.Core.Constants;
using ElderCare.Core.Entities;
using ElderCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/assessment/sarcopenia")]
[Authorize(Roles = Roles.Patient)]
public class SarcopeniaController : ControllerBase
{
    private readonly AppDbContext _context;

    public SarcopeniaController(AppDbContext context)
    {
        _context = context;
    }

    // ===== SARC-F =====
    [HttpPost("sarc-f")]
    public async Task<IActionResult> CreateSarcF(CreateSarcFDto dto)
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var total = dto.Strength + dto.Walking + dto.ChairRise + dto.Stairs + dto.Falls;

        var result = new SarcFResult
        {
            PatientId = patientId,
            Strength = dto.Strength,
            Walking = dto.Walking,
            ChairRise = dto.ChairRise,
            Stairs = dto.Stairs,
            Falls = dto.Falls,
            TotalScore = total,
            AtRisk = total >= 4
        };

        _context.SarcFResults.Add(result);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            result.TotalScore,
            result.AtRisk
        });
    }

    // ===== 5TSTS =====
    [HttpPost("5tsts")]
    public async Task<IActionResult> CreateFiveTsts(CreateFiveTstsDto dto)
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var probable = dto.TotalTimeSeconds > 15;

        var result = new FiveTstsResult
        {
            PatientId = patientId,
            TotalTimeSeconds = dto.TotalTimeSeconds,
            ValidReps = dto.ValidReps,
            ProbableSarcopenia = probable
        };

        _context.FiveTstsResults.Add(result);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            result.TotalTimeSeconds,
            result.ProbableSarcopenia
        });
    }
}