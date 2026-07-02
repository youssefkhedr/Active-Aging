namespace ElderCare.Core.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PasswordHash { get; set; }
    
    // Clerk OAuth integration - stores Clerk's user ID (e.g., "user_2abc123def456")
    public string? ClerkUserId { get; set; }
    public string? ProfilePictureUrl { get; set; }

    public string Role { get; set; } = "PATIENT";

    public int Age { get; set; }
    public string Gender { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
