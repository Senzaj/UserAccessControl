namespace UserManagementSystem.DTOs.RoleManagement;

public record SetPermissionRequest(Guid RoleId, Guid PermissionId);