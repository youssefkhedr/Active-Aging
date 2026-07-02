namespace ElderCare.Application.DTOs;

public class FallRiskPredictionDto
{
    public double RiskScore { get; set; }
    public string RiskLevel { get; set; } = null!; // Low / Medium / High
}
