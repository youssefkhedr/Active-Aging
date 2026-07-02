using ElderCare.Application.DTOs.Admin;

namespace ElderCare.Application.Interfaces;

public interface IAdminService
{
    Task<List<AdminUserDto>> GetAllUsersAsync();
    Task UpdateUserRoleAsync(Guid userId, string role);
    Task UpdateUserStatusAsync(Guid userId, bool isActive);
}
