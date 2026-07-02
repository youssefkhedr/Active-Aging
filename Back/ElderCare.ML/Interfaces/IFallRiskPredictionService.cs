using ElderCare.Application.DTOs;

namespace ElderCare.ML.Interfaces;

public interface IFallRiskPredictionService
{
    FallRiskPredictionDto Predict(double balanceIndex, int age, int mmseScore);
}
