namespace ElderCare.Application.DTOs.Patient;

public class PatientTreatmentPlanDto
{
    public string DoctorName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
