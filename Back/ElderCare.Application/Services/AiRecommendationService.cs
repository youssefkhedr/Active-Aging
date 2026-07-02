using ElderCare.Application.DTOs.AI;
using ElderCare.Application.Interfaces;

namespace ElderCare.Application.Services
{
    public class AiRecommendationService : IAiRecommendationService
    {
        public AiRecommendationDto Generate(Guid patientId)
        {
            var result = new AiRecommendationDto();

            // Simulated data (later from DB)
            var romStatus = "Limited";
            var balanceScore = 3.5;
            var sarcopeniaRisk = true;
            var mmse = 22;

            // ROM Rules
            if (romStatus == "Limited")
                result.PhysicalExercises.Add("Passive knee stretching");

            // Balance Rules
            if (balanceScore > 3)
                result.PhysicalExercises.Add("Static balance standing");

            // Sarcopenia Rules
            if (sarcopeniaRisk)
                result.PhysicalExercises.Add("Resistance band exercises");

            // Cognitive Rules
            if (mmse < 24)
                result.CognitiveExercises.Add("Memory recall games");

            // Safety
            if (balanceScore > 4)
                result.Warnings.Add("High fall risk - supervise exercises");

            return result;
        }
    }
}
