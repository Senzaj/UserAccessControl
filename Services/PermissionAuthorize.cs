using Microsoft.AspNetCore.Authorization;

namespace UserManagementSystem.Services;

public class PermissionAuthorize(string resource, string action) : AuthorizeAttribute("PermissionPolicy")
{
    public string Resource => resource;
    public string Action => action;
}