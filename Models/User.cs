using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models;

[Table("users")]
public class User
{
    [Key] 
    [Column("user_id")] 
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(255)]
    [Column("user_email")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Column("user_first_name")]
    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;
    
    [Column("user_last_name")]
    [MaxLength(255)]
    public string? LastName { get; set; } 
    
    [Column("user_is_active")]
    public bool IsActive { get; set; } = true;
    
    [Column("user_created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<UserRole>? UserRoles { get; set; }
}