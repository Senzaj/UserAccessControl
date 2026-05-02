namespace UserManagementSystem.DTOs;

public record RegisterRequest(string Email, string Password, string ConfirmPassword, string FirstName, string? LastName);