namespace ElderCare.Application.DTOs.Reports
{
    public class PatientReportDto
    {
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public string RomStatus { get; set; }
        public double BalanceScore { get; set; }

        public bool SarcopeniaRisk { get; set; }
        public int CognitiveScore { get; set; }

        public string PredictionSummary { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
