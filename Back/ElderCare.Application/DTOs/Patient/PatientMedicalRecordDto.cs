namespace ElderCare.Application.DTOs.Patient;

public class PatientMedicalRecordDto
{
    public string DoctorName { get; set; } = null!;
    public string Diagnosis { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
