namespace UserManagementSystem.DTOs.Authentication;

public record RegisterRequest(string Email, string Password, string ConfirmPassword, string FirstName, string? LastName);