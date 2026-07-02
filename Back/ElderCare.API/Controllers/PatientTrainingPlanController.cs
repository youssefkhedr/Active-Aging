using ElderCare.Core.Constants;
using ElderCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/training-plan")]
[Authorize(Roles = Roles.Patient)]
public class PatientTrainingPlanController : ControllerBase
{
    private readonly AppDbContext _context;

    public PatientTrainingPlanController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentPlan()
    {
        var patientId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var plan = await _context.TrainingPlans
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefaultAsync();

        if (plan == null)
            return NotFound("No training plan assigned");

        return Ok(plan);
    }
}