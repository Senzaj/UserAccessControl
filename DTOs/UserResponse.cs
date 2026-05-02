using UserManagementSystem.Models;

namespace UserManagementSystem.DTOs;

public record UserResponse(Guid Id, string Email, string FirstName, string? LastName,
    bool IsActive, DateTime CreatedAt, List<string> Roles);