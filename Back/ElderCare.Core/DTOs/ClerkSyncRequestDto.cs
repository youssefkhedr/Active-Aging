using System.ComponentModel.DataAnnotations;

namespace ElderCare.Core.DTOs;

public class ClerkSyncRequestDto
{
    [Required]
    public string ClerkToken { get; set; } = null!;
}
