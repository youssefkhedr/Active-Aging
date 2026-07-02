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
[Route("api/assessment/cognitive")]
[Authorize(Roles = Roles.Patient)]
public class CognitiveAssessmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public CognitiveAssessmentController(AppDbContext context)
    {
        _context = context;
    }

    // ===== MMSE =====
    [HttpPost("mmse")]
    public async Task<IActionResult> CreateMmse(CreateMmseDto dto)
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var result = new CognitiveMmseResult
        {
            PatientId = patientId,
            SectionScores = dto.SectionScores,
            TotalScore = dto.TotalScore
        };

        _context.CognitiveMmseResults.Add(result);
        await _context.SaveChangesAsync();

        return Ok(result);
    }

    // ===== MINI-COG =====
    [HttpPost("mini-cog")]
    public async Task<IActionResult> CreateMiniCog(CreateMiniCogDto dto)
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var result = new CognitiveMiniCogResult
        {
            PatientId = patientId,
            RecallScore = dto.RecallScore,
            ClockResult = dto.ClockResult
        };

        _context.CognitiveMiniCogResults.Add(result);
        await _context.SaveChangesAsync();

        return Ok(result);
    }

    // ===== HISTORY =====
    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var mmse = await _context.CognitiveMmseResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var miniCog = await _context.CognitiveMiniCogResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(new { mmse, miniCog });
    }
}