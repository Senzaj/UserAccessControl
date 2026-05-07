namespace UserManagementSystem.DTOs.Authentication;

public record AuthResponse (string Token, UserResponse User);