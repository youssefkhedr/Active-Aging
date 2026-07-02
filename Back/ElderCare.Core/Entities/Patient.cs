namespace ElderCare.Core.Entities;

public class Patient
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid? DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = null!;
    public string MedicalNotes { get; set; } = null!;
}
