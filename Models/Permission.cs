using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models;

[Table("permissions")]
public class Permission
{
    [Key]
    [Column("permission_id")]
    public Guid Id { get; set; } =  Guid.NewGuid();
    
    [Column("permission_resource")]
    [MaxLength(255)]
    public string Resource { get; set; } =  string.Empty;
    
    [Column("permission_action")]
    [MaxLength(255)]
    public string Action { get; set; } =  string.Empty;
    
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}