namespace ElderCare.Core.DTOs;

public class RegisterRequestDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Age { get; set; }
    public string Gender { get; set; } = null!;
}