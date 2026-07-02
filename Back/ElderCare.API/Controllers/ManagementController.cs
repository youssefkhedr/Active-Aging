using ElderCare.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/management")]
public class ManagementController : ControllerBase
{
    [HttpGet("reports")]
    [Authorize(Roles = Roles.Doctor + "," + Roles.Admin)]
    public IActionResult GetReports()
    {
        // Both doctors and admins can access reports
        return Ok("Reports accessed by authorized personnel");
    }

    [HttpPost("system-config")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult UpdateSystemConfig()
    {
        // Only admins can update system config
        return Ok("System configuration updated");
    }

    [HttpGet("public-info")]
    [AllowAnonymous]
    public IActionResult GetPublicInfo()
    {
        // No authorization required
        return Ok("Public information available to everyone");
    }
}