using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserManagementSystem.Configuration;
using UserManagementSystem.Data;
using UserManagementSystem.DTOs;
using UserManagementSystem.DTOs.Authentication;
using UserManagementSystem.Models;
using UserManagementSystem.Services.Interfaces;

namespace UserManagementSystem.Services;

public class AuthService(AppDbContext dbContext, IOptions<JwtSettings> jwtOptions) : IAuthService
{
    private AppDbContext _dbContext = dbContext;
    private JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            throw new Exception("Passwords do not match");

        if (_dbContext.Users.Any(u => u.Email == request.Email))
            throw new Exception("Email is already in use");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User
        {
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = passwordHash,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return new AuthResponse(GenerateJwtToken(user), await GetUserById(user.Id));
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _dbContext.Users
                       .Include(u => u.UserRoles)!
                       .ThenInclude(ur => ur.Role)
                       .SingleOrDefaultAsync(u => u.Email == request.Email)
                    ??  throw new InvalidOperationException("No user found");
        
        if (!user.IsActive)
            throw new InvalidOperationException("User is deleted");
            
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new InvalidOperationException("Passwords do not match");
        
        return new AuthResponse(GenerateJwtToken(user), await GetUserById(user.Id));
    }

    public async Task<UserResponse> GetUserById(Guid id)
    {
        var user = await _dbContext.Users.
                       Include(u => u.UserRoles)!
                       .ThenInclude(ur => ur.Role)
                       .SingleAsync(u => u.Id == id)
                    ?? throw new KeyNotFoundException();
        
        return new UserResponse(user.Id, user.Email, user.FirstName, user.LastName,
            user.IsActive, user.CreatedAt, (user.UserRoles ?? throw new ArgumentNullException())
            .Select(ur => ur.Role.Name).ToList());
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
        };
        
        if (user.UserRoles is not null)
            claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name)));

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.ExpirationInMinutes)),
            signingCredentials: new SigningCredentials(_jwtSettings.GetSymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}