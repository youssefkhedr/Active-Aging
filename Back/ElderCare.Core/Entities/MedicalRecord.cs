namespace ElderCare.Core.Entities;

public class MedicalRecord
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public string Diagnosis { get; set; } = null!;
    public string Notes { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
