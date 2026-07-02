using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateTrainingPlanDto
{
    [Required]
    public Guid PatientId { get; set; }

    [Required]
    public string Exercises { get; set; } = null!;

    [Range(1, 20)]
    public int Sets { get; set; }

    [Range(1, 50)]
    public int Reps { get; set; }

    [Required]
    public string Schedule { get; set; } = null!;
}