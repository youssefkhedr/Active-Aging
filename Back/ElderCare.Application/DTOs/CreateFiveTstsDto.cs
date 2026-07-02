using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateFiveTstsDto
{
    [Range(0, 60)]
    public double TotalTimeSeconds { get; set; }

    [Range(1, 5)]
    public int ValidReps { get; set; }
}