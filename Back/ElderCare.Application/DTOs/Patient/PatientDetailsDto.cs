namespace ElderCare.Application.DTOs.Patient
{
    public class PatientDetailsDto
    {
        public Guid PatientId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }

        public RomStatusDto LatestRom { get; set; }
        public BalanceStatusDto LatestBalance { get; set; }
        public SarcopeniaStatusDto Sarcopenia { get; set; }
        public CognitiveStatusDto Cognitive { get; set; }
        public TrainingPlanDto TrainingPlan { get; set; }
    }
}
