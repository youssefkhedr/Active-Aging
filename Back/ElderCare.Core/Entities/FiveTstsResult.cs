using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class FiveTstsResult
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public double TotalTimeSeconds { get; set; }
    public int ValidReps { get; set; }

    public bool ProbableSarcopenia { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}