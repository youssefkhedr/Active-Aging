namespace ElderCare.Application.DTOs.Doctor;

public class CreateTreatmentPlanDto
{
    public Guid PatientId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
