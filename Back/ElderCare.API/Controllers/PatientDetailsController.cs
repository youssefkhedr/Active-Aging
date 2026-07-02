using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers
{
    [ApiController]
    [Route("api/doctor/patient")]
    [Authorize(Roles = "Doctor")]
    public class PatientDetailsController : ControllerBase
    {
        private readonly IPatientDetailsService _service;

        public PatientDetailsController(IPatientDetailsService service)
        {
            _service = service;
        }

        [HttpGet("{patientId}")]
        public IActionResult GetPatientDetails(Guid patientId)
        {
            var result = _service.GetPatientDetails(patientId);
            return Ok(result);
        }
    }
}
