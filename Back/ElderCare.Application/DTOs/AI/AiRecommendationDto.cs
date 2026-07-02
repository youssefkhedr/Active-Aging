namespace ElderCare.Application.DTOs.AI
{
    public class AiRecommendationDto
    {
        public List<string> PhysicalExercises { get; set; } = new();
        public List<string> CognitiveExercises { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
    }
}
