using ElderCare.Application.DTOs.Doctor;
using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using ElderCare.Core.Constants;
namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/doctor")]
[Authorize(Roles = Roles.Doctor)]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet("patients")]
    public async Task<IActionResult> GetMyPatients()
    {
        return Ok(await _doctorService.GetMyPatientsAsync(GetUserId()));
    }
}