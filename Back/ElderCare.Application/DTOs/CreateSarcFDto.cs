using System.ComponentModel.DataAnnotations;

namespace ElderCare.Application.DTOs;

public class CreateSarcFDto
{
    [Range(0, 2)] public int Strength { get; set; }
    [Range(0, 2)] public int Walking { get; set; }
    [Range(0, 2)] public int ChairRise { get; set; }
    [Range(0, 2)] public int Stairs { get; set; }
    [Range(0, 2)] public int Falls { get; set; }
}