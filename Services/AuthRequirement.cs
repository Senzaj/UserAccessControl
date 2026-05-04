using Microsoft.AspNetCore.Authorization;

namespace UserManagementSystem.Services;

public class AuthRequirement: IAuthorizationRequirement
{
    public  string? Resource {get;set;}
    public  string? Action {get; set;}
}