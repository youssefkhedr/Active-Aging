namespace ElderCare.Application.DTOs.Prediction
{
    public class PredictionResultDto
    {
        public bool HighFallRisk { get; set; }
        public bool SarcopeniaRisk { get; set; }
        public string RiskLevel { get; set; }
    }
}
