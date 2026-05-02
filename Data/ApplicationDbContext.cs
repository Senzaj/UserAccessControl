using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Models;

namespace UserManagementSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Role>()
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Role>()
            .HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Permission>()
            .HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
        ;

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();

        GenerateSeedData(modelBuilder);
    }

    private void GenerateSeedData(ModelBuilder modelBuilder)
    {
        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var seniorRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var middleRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var juniorRoleId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        var adminUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var seniorUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var middleUserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var juniorUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

        var projRead = Guid.Parse("20000000-0000-0000-0000-000000000001");
        var projCreate = Guid.Parse("20000000-0000-0000-0000-000000000002");
        var projUpdate = Guid.Parse("20000000-0000-0000-0000-000000000003");
        var projDelete = Guid.Parse("20000000-0000-0000-0000-000000000004");

        var taskRead = Guid.Parse("30000000-0000-0000-0000-000000000001");
        var taskCreate = Guid.Parse("30000000-0000-0000-0000-000000000002");
        var taskUpdate = Guid.Parse("30000000-0000-0000-0000-000000000003");
        var taskDelete = Guid.Parse("30000000-0000-0000-0000-000000000004");

        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = projRead, Resource = "Project", Action = "Read" },
            new Permission { Id = projCreate, Resource = "Project", Action = "Create" },
            new Permission { Id = projUpdate, Resource = "Project", Action = "Update" },
            new Permission { Id = projDelete, Resource = "Project", Action = "Delete" },
            new Permission { Id = taskRead, Resource = "Task", Action = "Read" },
            new Permission { Id = taskCreate, Resource = "Task", Action = "Create" },
            new Permission { Id = taskUpdate, Resource = "Task", Action = "Update" },
            new Permission { Id = taskDelete, Resource = "Task", Action = "Delete" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = "Admin", Description = "Full access to all resources" },
            new Role { Id = seniorRoleId, Name = "Senior", Description = "CRU on projects and tasks (no delete)" },
            new Role { Id = middleRoleId, Name = "Middle", Description = "Read projects; read, create tasks" },
            new Role { Id = juniorRoleId, Name = "Junior", Description = "Read tasks only" }
        );

        modelBuilder.Entity<RolePermission>().HasData(
            new RolePermission { RoleId = adminRoleId, PermissionId = projCreate },
            new RolePermission { RoleId = adminRoleId, PermissionId = projRead },
            new RolePermission { RoleId = adminRoleId, PermissionId = projUpdate },
            new RolePermission { RoleId = adminRoleId, PermissionId = projDelete },
            new RolePermission { RoleId = adminRoleId, PermissionId = taskCreate },
            new RolePermission { RoleId = adminRoleId, PermissionId = taskRead },
            new RolePermission { RoleId = adminRoleId, PermissionId = taskUpdate },
            new RolePermission { RoleId = adminRoleId, PermissionId = taskDelete },

            new RolePermission { RoleId = seniorRoleId, PermissionId = projCreate },
            new RolePermission { RoleId = seniorRoleId, PermissionId = projRead },
            new RolePermission { RoleId = seniorRoleId, PermissionId = projUpdate },
            new RolePermission { RoleId = seniorRoleId, PermissionId = taskCreate },
            new RolePermission { RoleId = seniorRoleId, PermissionId = taskRead },
            new RolePermission { RoleId = seniorRoleId, PermissionId = taskUpdate },

            new RolePermission { RoleId = middleRoleId, PermissionId = projRead },
            new RolePermission { RoleId = middleRoleId, PermissionId = taskCreate },
            new RolePermission { RoleId = middleRoleId, PermissionId = taskRead },
            new RolePermission { RoleId = middleRoleId, PermissionId = taskUpdate },

            new RolePermission { RoleId = juniorRoleId, PermissionId = taskRead },
            new RolePermission { RoleId = juniorRoleId, PermissionId = taskUpdate }
        );
        
        //TODO: Хэши паролей (плейсхолдеры, сгенерировать реальные через BCrypt)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUserId,
                Email = "admin@example.com",
                PasswordHash = "$2a$11$K7sFYXGVg6QF4e7jCzMhL.u1VYB1Z0B8k9xVqmXvqXRGc5b8YJdZa", // Admin@123
                FirstName = "Admin",
                LastName = "User",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = seniorUserId,
                Email = "senior@example.com",
                PasswordHash = "$2a$11$...", // Senior@123
                FirstName = "Senior",
                LastName = "User",
                IsActive = true,
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = middleUserId,
                Email = "middle@example.com",
                PasswordHash = "$2a$11$...", // Middle@123
                FirstName = "Middle",
                LastName = "User",
                IsActive = true,
                CreatedAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = juniorUserId,
                Email = "junior@example.com",
                PasswordHash = "$2a$11$...", // Junior@123
                FirstName = "Junior",
                LastName = "User",
                IsActive = true,
                CreatedAt = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = adminUserId, RoleId = adminRoleId },
            new UserRole { UserId = seniorUserId, RoleId = seniorRoleId },
            new UserRole { UserId = middleUserId, RoleId = middleRoleId },
            new UserRole { UserId = juniorUserId, RoleId = juniorRoleId }
        );
    }
}