using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers
{
    [ApiController]
    [Route("api/ai/recommendations")]
    [Authorize(Roles = "Doctor")]
    public class AiRecommendationController : ControllerBase
    {
        private readonly IAiRecommendationService _service;

        public AiRecommendationController(IAiRecommendationService service)
        {
            _service = service;
        }

        [HttpGet("{patientId}")]
        public IActionResult Get(Guid patientId)
        {
            return Ok(_service.Generate(patientId));
        }
    }
}
