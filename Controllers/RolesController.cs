using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;
using UserManagementSystem.DTOs.RoleManagement;
using UserManagementSystem.Models;
using UserManagementSystem.Services;

namespace UserManagementSystem.Controllers;


[ApiController]
[PermissionAuthorize("Role", "Manage")]
[Route("api/[controller]")]
public class RolesController(AppDbContext dbContext): ControllerBase
{
    [HttpGet("getRoles")]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            var roles = new RolesResponse(await dbContext.Roles.Select(r => r.Name).ToListAsync());
            return Ok(roles);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }
    
    [HttpGet("getRole/{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRole(Guid id)
    {
        try
        {
            var role = new RoleResponse(await dbContext.Roles.Where(r => r.Id == id).Select(r => r.Name).SingleAsync());
            return Ok(role);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }

    [HttpPost("createRole")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateRole(CreateRoleRequest request)
    {
        try
        {
            await dbContext.AddAsync(new Role(request.Name, request.Description));
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("deleteRole/{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        try
        {
            dbContext.Roles.Remove(await dbContext.Roles.Where(r => r.Id == id).SingleAsync());
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }

    [HttpGet("getPermissions")]
    public async Task<IActionResult> GetPermissions()
    {
        try
        {
            var permissionResponse = new PermissionsResponse(await dbContext.Permissions
                    .Select(r => new RolePrimitive(r.Id ,r.Resource, r.Action)).ToListAsync());
            return Ok(permissionResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }

    [HttpPost("setPermission")]
    public async Task<IActionResult> SetPermission(SetPermissionRequest request)
    {
        try
        {
            await dbContext.RolePermissions.AddAsync(new RolePermission(request.RoleId, request.PermissionId));
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }
    
    [HttpDelete("revokePermission")]
    public async Task<IActionResult> RevokePermission(RevokePermissionRequest request)
    {
        try
        {
            dbContext.RolePermissions
                .Remove(await dbContext.RolePermissions
                    .Where(rp => rp.RoleId == request.RoleId && rp.PermissionId == request.PermissionId).SingleAsync());
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(404, e.Message);
        }
    }
}