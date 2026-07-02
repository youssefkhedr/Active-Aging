using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using ElderCare.Core.Constants;
namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/doctor/dashboard")]
[Authorize(Roles = Roles.Doctor)]
public class DoctorDashboardController : ControllerBase
{
    private readonly IDoctorDashboardService _service;

    public DoctorDashboardController(IDoctorDashboardService service)
    {
        _service = service;
    }

    [HttpGet("patients")]
    public IActionResult GetPatients()
    {
        // We can get the doctorId from claims if needed, but the current mock service implementation
        // takes a doctorId that isn't really used for filtering yet.
        // For now, let's just parse it to satisfy the signature.
        if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var doctorId))
        {
             var result = _service.GetPatients(doctorId);
             return Ok(result);
        }
        
        // Fallback or error if ID claim is missing (though Authorize guards this mostly)
        return Unauthorized("User ID not found in token");
    }
}
