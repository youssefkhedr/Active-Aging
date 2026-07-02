using ElderCare.Application.DTOs.Prediction;

namespace ElderCare.Application.Services.Prediction
{
    public class PredictionService
    {
        public PredictionResultDto Predict(PredictionRequestDto dto)
        {
            var fallRisk = dto.BalanceScore < 40 || dto.RomScore < 45;
            var sarcopenia = dto.SarcopeniaScore >= 4;

            return new PredictionResultDto
            {
                HighFallRisk = fallRisk,
                SarcopeniaRisk = sarcopenia,
                RiskLevel = fallRisk || sarcopenia ? "High" : "Low"
            };
        }
    }
}
