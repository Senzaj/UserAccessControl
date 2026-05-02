using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models;

[Table("role_permissions")]
public class RolePermission
{
    [Column("role_id")]
    public Guid RoleId { get; set; }
    
    [Column("permission_id")]
    public Guid PermissionId { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; }
    
    [ForeignKey(nameof(PermissionId))]
    public virtual Permission Permission { get; set; }
}