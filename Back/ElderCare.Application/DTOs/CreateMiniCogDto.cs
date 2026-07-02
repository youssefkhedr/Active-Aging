using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateMiniCogDto
{
    [Range(0, 3)]
    public int RecallScore { get; set; }

    [Required]
    public string ClockResult { get; set; } = null!;
}