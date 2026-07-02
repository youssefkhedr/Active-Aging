using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateMmseDto
{
    [Required]
    public string SectionScores { get; set; } = null!;

    [Range(0, 30)]
    public int TotalScore { get; set; }
}