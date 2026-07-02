using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using ElderCare.Core.Constants;
namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/patient")]
[Authorize(Roles = Roles.Patient)]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet("medical-records")]
    public async Task<IActionResult> GetMedicalRecords()
    {
        return Ok(await _patientService.GetMedicalRecordsAsync(GetUserId()));
    }

    [HttpGet("treatment-plans")]
    public async Task<IActionResult> GetTreatmentPlans()
    {
        return Ok(await _patientService.GetTreatmentPlansAsync(GetUserId()));
    }
}
