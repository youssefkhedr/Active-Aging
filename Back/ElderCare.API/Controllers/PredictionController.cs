using ElderCare.Application.Services.Prediction;
using ElderCare.Application.DTOs.Prediction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers
{
    [ApiController]
    [Route("api/prediction")]
    [Authorize(Roles = "Doctor,Admin")]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _service;

        public PredictionController(PredictionService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Predict([FromBody] PredictionRequestDto dto)
        {
            var result = _service.Predict(dto);
            return Ok(result);
        }
    }
}
