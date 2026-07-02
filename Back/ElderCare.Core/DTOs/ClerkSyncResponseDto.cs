namespace ElderCare.Core.DTOs;

public class ClerkSyncResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public ClerkUserDto User { get; set; } = null!;
    public bool IsNewUser { get; set; }
}

public class ClerkUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; }
}
