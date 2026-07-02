using ElderCare.Application.DTOs.AI;

namespace ElderCare.Application.Interfaces
{
    public interface IAiRecommendationService
    {
        AiRecommendationDto Generate(Guid patientId);
    }
}
