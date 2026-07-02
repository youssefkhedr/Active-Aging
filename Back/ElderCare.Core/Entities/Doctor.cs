namespace ElderCare.Core.Entities;

public class Doctor
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Specialization { get; set; } = null!;
}
