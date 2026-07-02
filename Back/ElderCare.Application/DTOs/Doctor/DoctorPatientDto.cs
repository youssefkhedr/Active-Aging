namespace ElderCare.Application.DTOs.Doctor;

public class DoctorPatientDto
{
    public Guid PatientId { get; set; }
    public string FullName { get; set; } = null!;
    public int Age { get; set; }
    public string Gender { get; set; } = null!;
    public string RiskLevel { get; set; } = "Low"; // Default value
}
