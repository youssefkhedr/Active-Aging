using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class RomAssessment
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public string JointType { get; set; } = null!;
    // shoulder, knee, hip, ankle, spine

    public double MaxAngle { get; set; }
    public double MinAngle { get; set; }

    public string Status { get; set; } = null!;
    // normal / limited / restricted

    public Guid SessionId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}