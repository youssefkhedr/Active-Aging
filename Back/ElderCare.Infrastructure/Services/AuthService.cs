using ElderCare.Core.DTOs;
using ElderCare.Core.Entities;
using ElderCare.Core.Interfaces;
using ElderCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

using ElderCare.Core.Constants;
namespace ElderCare.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        _httpClient = new HttpClient();
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("Email already exists");

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Age = dto.Age,
            Gender = dto.Gender,
            Role = Roles.Patient
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return await GenerateToken(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || string.IsNullOrEmpty(user.PasswordHash) || 
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return await GenerateToken(user);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == dto.RefreshToken);

        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new Exception("Invalid or expired refresh token");

        return await GenerateToken(user);
    }

    public async Task<ClerkSyncResponseDto> ClerkSyncAsync(ClerkSyncRequestDto dto)
    {
        // Decode and validate the Clerk JWT token
        var clerkClaims = await ValidateClerkTokenAsync(dto.ClerkToken);
        
        var clerkUserId = clerkClaims.Sub;
        var email = clerkClaims.Email;
        var fullName = clerkClaims.Name ?? email.Split('@')[0];
        var profilePictureUrl = clerkClaims.Picture;
        
        // Check if user exists by ClerkUserId or Email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.ClerkUserId == clerkUserId || u.Email == email);
        
        var isNewUser = user == null;
        
        if (isNewUser)
        {
            // Create new user
            user = new User
            {
                FullName = fullName,
                Email = email,
                ClerkUserId = clerkUserId,
                ProfilePictureUrl = profilePictureUrl,
                Role = Roles.Patient,
                Age = 0, // Default - can be updated later via profile
                Gender = "Not specified"
            };
            _context.Users.Add(user);
        }
        else
        {
            // Update existing user with Clerk data
            user!.ClerkUserId = clerkUserId;
            user.ProfilePictureUrl = profilePictureUrl;
            if (string.IsNullOrEmpty(user.FullName) || user.FullName == email.Split('@')[0])
            {
                user.FullName = fullName;
            }
        }
        
        await _context.SaveChangesAsync();
        
        // Generate backend JWT token
        var authResponse = await GenerateToken(user);
        
        return new ClerkSyncResponseDto
        {
            AccessToken = authResponse.AccessToken,
            RefreshToken = authResponse.RefreshToken,
            ExpiresAt = authResponse.ExpiresAt,
            IsNewUser = isNewUser,
            User = new ClerkUserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                ProfilePictureUrl = user.ProfilePictureUrl
            }
        };
    }
    
    private async Task<ClerkTokenClaims> ValidateClerkTokenAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        // Read the token without validation first to get the issuer
        if (!handler.CanReadToken(token))
            throw new Exception("Invalid token format");
        
        var jwtToken = handler.ReadJwtToken(token);
        
        // For production, you should validate against Clerk's JWKS endpoint
        // The issuer format is typically: https://<your-clerk-frontend-api>.clerk.accounts.dev
        // Or your custom domain if configured
        
        var clerkSecretKey = _config["Clerk:SecretKey"];
        
        if (string.IsNullOrEmpty(clerkSecretKey))
        {
            // If no secret key configured, do basic validation (development mode)
            // WARNING: In production, always validate the token signature!
            var claims = jwtToken.Claims.ToList();
            
            return new ClerkTokenClaims
            {
                Sub = claims.FirstOrDefault(c => c.Type == "sub")?.Value 
                    ?? throw new Exception("Missing sub claim"),
                Email = claims.FirstOrDefault(c => c.Type == "email")?.Value 
                    ?? throw new Exception("Missing email claim"),
                Name = claims.FirstOrDefault(c => c.Type == "name")?.Value,
                Picture = claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                EmailVerified = claims.FirstOrDefault(c => c.Type == "email_verified")?.Value == "true"
            };
        }
        
        // Production validation using Clerk's JWKS
        var issuer = _config["Clerk:Issuer"] ?? jwtToken.Issuer;
        var jwksUrl = $"{issuer}/.well-known/jwks.json";
        
        try
        {
            var jwksJson = await _httpClient.GetStringAsync(jwksUrl);
            var jwks = new JsonWebKeySet(jwksJson);
            
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = false, // Clerk tokens may not have audience
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = jwks.Keys
            };
            
            var principal = handler.ValidateToken(token, validationParameters, out _);
            
            return new ClerkTokenClaims
            {
                Sub = principal.FindFirst("sub")?.Value ?? throw new Exception("Missing sub claim"),
                Email = principal.FindFirst("email")?.Value ?? throw new Exception("Missing email claim"),
                Name = principal.FindFirst("name")?.Value,
                Picture = principal.FindFirst("picture")?.Value,
                EmailVerified = principal.FindFirst("email_verified")?.Value == "true"
            };
        }
        catch (HttpRequestException)
        {
            throw new Exception("Unable to validate token - could not reach Clerk JWKS endpoint");
        }
    }

    private async Task<AuthResponseDto> GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_config["Jwt:ExpireMinutes"]!)
            ),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var refreshToken = Guid.NewGuid().ToString();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            ExpiresAt = token.ValidTo
        };
    }
}

// Helper class to hold parsed Clerk claims
internal class ClerkTokenClaims
{
    public string Sub { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public bool EmailVerified { get; set; }
}
