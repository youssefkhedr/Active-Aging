using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class CognitiveMiniCogResult
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public int RecallScore { get; set; } // 0–3
    public string ClockResult { get; set; } = null!;
    // normal / abnormal

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}