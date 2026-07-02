using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateRomAssessmentDto
{
    [Required]
    public string JointType { get; set; } = null!;

    public double MaxAngle { get; set; }
    public double MinAngle { get; set; }

    [Required]
    public Guid SessionId { get; set; }
}