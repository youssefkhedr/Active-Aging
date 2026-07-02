using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class CognitiveMmseResult
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public string SectionScores { get; set; } = null!;
    // JSON string

    public int TotalScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}