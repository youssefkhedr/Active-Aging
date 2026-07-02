namespace ElderCare.Application.DTOs.AI;

public class RecommendationDto
{
    public string Category { get; set; } = null!; // Physical | Cognitive | Risk
    public string Message { get; set; } = null!;
}
