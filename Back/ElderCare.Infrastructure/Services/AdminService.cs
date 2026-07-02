using ElderCare.Application.DTOs.Admin;
using ElderCare.Application.Interfaces;
using ElderCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ElderCare.Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdminUserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(u => new AdminUserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    }

    public async Task UpdateUserRoleAsync(Guid userId, string role)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception("User not found");

        user.Role = role;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserStatusAsync(Guid userId, bool isActive)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception("User not found");

        user.IsActive = isActive;
        await _context.SaveChangesAsync();
    }
}
