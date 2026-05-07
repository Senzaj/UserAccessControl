using UserManagementSystem.Models;

namespace UserManagementSystem.DTOs.RoleManagement;

public record PermissionsResponse(List<RolePrimitive> Permissions);