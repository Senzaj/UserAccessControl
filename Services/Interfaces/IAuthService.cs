using UserManagementSystem.DTOs;
using UserManagementSystem.DTOs.Authentication;

namespace UserManagementSystem.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<UserResponse> GetUserById(Guid id);
}