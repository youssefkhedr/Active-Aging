using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateBalanceAssessmentDto
{
    public double SwayScore { get; set; }
    public double StabilityIndex { get; set; }

    [Required]
    public string TestType { get; set; } = null!;
}