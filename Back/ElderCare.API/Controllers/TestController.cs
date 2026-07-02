using ElderCare.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("admin")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult AdminOnly()
    {
        return Ok("Admin Access Granted");
    }

    [HttpGet("doctor")]
    [Authorize(Roles = Roles.Doctor)]
    public IActionResult DoctorOnly()
    {
        return Ok("Doctor Access Granted");
    }

    [HttpGet("patient")]
    [Authorize(Roles = Roles.Patient)]
    public IActionResult PatientOnly()
    {
        return Ok("Patient Access Granted");
    }

    [HttpGet("any")]
    [Authorize]
    public IActionResult AnyAuthenticatedUser()
    {
        return Ok("Any Logged-in User");
    }
}
