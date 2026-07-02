using ElderCare.ML.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/predictions")]
[Authorize(Roles = "Doctor,Admin")]
public class PredictionsController : ControllerBase
{
    private readonly IFallRiskPredictionService _prediction;

    public PredictionsController(IFallRiskPredictionService prediction)
    {
        _prediction = prediction;
    }

    [HttpGet("fall-risk")]
    public IActionResult Predict(
        [FromQuery] double balanceIndex,
        [FromQuery] int age,
        [FromQuery] int mmseScore)
    {
        var result = _prediction.Predict(balanceIndex, age, mmseScore);
        return Ok(result);
    }
}
