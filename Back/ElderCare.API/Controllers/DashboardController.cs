using ElderCare.Core.Constants;
using ElderCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // 👴 PATIENT DASHBOARD
    // =========================
    [HttpGet("patient")]
    [Authorize(Roles = Roles.Patient)]
    public async Task<IActionResult> PatientDashboard()
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        // ===== ROM =====
        var latestRom = await _context.RomAssessments
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new
            {
                x.JointType,
                x.Status,
                x.CreatedAt
            })
            .FirstOrDefaultAsync();

        // ===== Balance =====
        var latestBalance = await _context.BalanceAssessments
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new
            {
                x.SwayScore,
                x.StabilityIndex,
                x.CreatedAt
            })
            .FirstOrDefaultAsync();

        // ===== Sarcopenia =====
        var sarcF = await _context.SarcFResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        var fiveTsts = await _context.FiveTstsResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        // ===== Cognitive =====
        var mmseHistory = await _context.CognitiveMmseResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        var miniCogHistory = await _context.CognitiveMiniCogResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        return Ok(new
        {
            rom = latestRom,
            balance = latestBalance,
            sarcopenia = new
            {
                sarcF = sarcF == null ? null : new
                {
                    sarcF.TotalScore,
                    sarcF.AtRisk
                },
                fiveTsts = fiveTsts == null ? null : new
                {
                    fiveTsts.TotalTimeSeconds,
                    fiveTsts.ProbableSarcopenia
                }
            },
            cognitive = new
            {
                mmseHistory,
                miniCogHistory
            }
        });
    }

    // =========================
    // 👨‍⚕️ DOCTOR DASHBOARD
    // =========================
    [HttpGet("doctor/{patientId}")]
    [Authorize(Roles = Roles.Doctor + "," + Roles.Admin)]
    public async Task<IActionResult> DoctorDashboard(Guid patientId)
    {
        var user = await _context.Users
            .Where(x => x.Id == patientId)
            .Select(x => new
            {
                x.Id,
                x.FullName,
                x.Age,
                x.Gender
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound("Patient not found");

        var rom = await _context.RomAssessments
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var balance = await _context.BalanceAssessments
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var sarcF = await _context.SarcFResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var fiveTsts = await _context.FiveTstsResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var mmse = await _context.CognitiveMmseResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var miniCog = await _context.CognitiveMiniCogResults
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(new
        {
            patient = user,
            rom,
            balance,
            sarcopenia = new
            {
                sarcF,
                fiveTsts
            },
            cognitive = new
            {
                mmse,
                miniCog
            }
        });
    }
}