using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models;

[Table("user_roles")]
public class UserRole
{
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("role_id")]
    public Guid RoleId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; }
}