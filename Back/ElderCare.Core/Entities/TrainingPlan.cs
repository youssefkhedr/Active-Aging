using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.Entities;

public class TrainingPlan
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }

    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public string Exercises { get; set; } = null!;
    // JSON string (simple + flexible)

    public int Sets { get; set; }
    public int Reps { get; set; }

    public string Schedule { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}