using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UserManagementSystem.Services;

public class PermissionAuthorizationHandler(RolePermissionCache rolePermissionCache): AuthorizationHandler<AuthRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AuthRequirement requirement)
    {
        Console.WriteLine($"Auth: {context.User.Identity?.IsAuthenticated}, Claims: {string.Join(", ", context.User.Claims.Select(c => c.Type + "=" + c.Value))}");
        
        var httpContext = (HttpContext)context.Resource!;
        var endPoint = httpContext.GetEndpoint();
        var attribute = endPoint.Metadata.GetMetadata<PermissionAuthorize>();

        var roles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        if (await rolePermissionCache.
                HasPermissionAsync(roles, attribute.Resource, attribute.Action))
            context.Succeed(requirement);
    }
}