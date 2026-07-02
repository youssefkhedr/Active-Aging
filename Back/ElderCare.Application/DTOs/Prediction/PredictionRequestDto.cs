namespace ElderCare.Application.DTOs.Prediction
{
    public class PredictionRequestDto
    {
        public int PatientId { get; set; }
        public double BalanceScore { get; set; }
        public double RomScore { get; set; }
        public int SarcopeniaScore { get; set; }
        public int CognitiveScore { get; set; }
    }
}
