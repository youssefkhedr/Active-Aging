namespace ElderCare.Application.DTOs.Doctor;

public class CreateMedicalRecordDto
{
    public Guid PatientId { get; set; }
    public string Diagnosis { get; set; } = null!;
    public string Notes { get; set; } = null!;
}
