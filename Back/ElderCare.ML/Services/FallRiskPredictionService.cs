using ElderCare.Application.DTOs;
using ElderCare.ML.Interfaces;

namespace ElderCare.ML.Services;

public class FallRiskPredictionService : IFallRiskPredictionService
{
    public FallRiskPredictionDto Predict(double balanceIndex, int age, int mmseScore)
    {
        double score = 0;

        if (balanceIndex < 50) score += 40;
        if (age > 70) score += 30;
        if (mmseScore < 24) score += 30;

        string level = score switch
        {
            < 30 => "Low",
            < 70 => "Medium",
            _ => "High"
        };

        return new FallRiskPredictionDto
        {
            RiskScore = score,
            RiskLevel = level
        };
    }
}
