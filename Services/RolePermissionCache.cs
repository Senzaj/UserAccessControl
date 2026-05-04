using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;

namespace UserManagementSystem.Services;

public class RolePermissionCache(AppDbContext dbContext)
{
    public async Task<bool> HasPermissionAsync(List<string> roleNames, string? resource, string action)
    {
        try
        {
            var rolePermissions = await dbContext.RolePermissions
                .Where(rp => rp.Permission.Resource == resource && rp.Permission.Action == action)
                .Include(rp => rp.Role)
                .ToListAsync();

            return rolePermissions.Any(rp => roleNames.Contains(rp.Role.Name));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}