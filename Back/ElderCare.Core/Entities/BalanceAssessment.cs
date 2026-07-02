using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class BalanceAssessment
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public double SwayScore { get; set; }
    public double StabilityIndex { get; set; }

    public string TestType { get; set; } = null!;
    // single_leg / static

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}