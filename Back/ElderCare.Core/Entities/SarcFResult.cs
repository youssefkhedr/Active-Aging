using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class SarcFResult
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public int Strength { get; set; }
    public int Walking { get; set; }
    public int ChairRise { get; set; }
    public int Stairs { get; set; }
    public int Falls { get; set; }

    public int TotalScore { get; set; }
    public bool AtRisk { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}