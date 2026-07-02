using ElderCare.Application.DTOs;
using ElderCare.Core.Constants;
using ElderCare.Core.Entities;
using ElderCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/doctor/training-plan")]
[Authorize(Roles = Roles.Doctor)]
public class DoctorTrainingPlanController : ControllerBase
{
    private readonly AppDbContext _context;

    public DoctorTrainingPlanController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlan(CreateTrainingPlanDto dto)
    {
        var doctorId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var plan = new TrainingPlan
        {
            PatientId = dto.PatientId,
            DoctorId = doctorId,
            Exercises = dto.Exercises,
            Sets = dto.Sets,
            Reps = dto.Reps,
            Schedule = dto.Schedule
        };

        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();

        return Ok(plan);
    }
}