using ElderCare.Core.DTOs;

namespace ElderCare.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto);
    Task<ClerkSyncResponseDto> ClerkSyncAsync(ClerkSyncRequestDto dto);
}