using ElderCare.Application.DTOs.Patient;
using ElderCare.Application.Interfaces;

namespace ElderCare.Application.Services
{
    public class PatientDetailsService : IPatientDetailsService
    {
        public PatientDetailsDto GetPatientDetails(Guid patientId)
        {
            return new PatientDetailsDto
            {
                PatientId = patientId,
                FullName = "Ahmed Ali",
                Age = 72,

                LatestRom = new RomStatusDto
                {
                    JointType = "Knee",
                    Status = "Limited",
                    Date = DateTime.UtcNow.AddDays(-2)
                },

                LatestBalance = new BalanceStatusDto
                {
                    SwayScore = 3.2,
                    Stability = "Moderate",
                    Date = DateTime.UtcNow.AddDays(-1)
                },

                Sarcopenia = new SarcopeniaStatusDto
                {
                    AtRisk = true,
                    Probable = false
                },

                Cognitive = new CognitiveStatusDto
                {
                    MmseScore = 23,
                    MiniCogResult = "Abnormal"
                },

                TrainingPlan = new TrainingPlanDto
                {
                    Schedule = "3x per week",
                    Exercises = new List<string> { "Squats", "Balance Walk", "Memory Cards" }
                }
            };
        }
    }
}
