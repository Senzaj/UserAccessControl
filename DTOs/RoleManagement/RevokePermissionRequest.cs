namespace UserManagementSystem.DTOs.RoleManagement;

public record RevokePermissionRequest(Guid RoleId, Guid PermissionId);