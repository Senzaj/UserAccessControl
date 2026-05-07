using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models;

[Table("roles")]
public class Role()
{
    public Role(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }

    [Key]
    [Column("role_id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("role_name")]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    [Column("role_description")]
    [MaxLength(255)]
    public string? Description { get; set; } = string.Empty;
    
    
    public virtual ICollection<UserRole> UserRoles {get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}