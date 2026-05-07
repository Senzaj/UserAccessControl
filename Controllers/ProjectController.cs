using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Services;

namespace UserManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController: ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [PermissionAuthorize("Project", "Read")]
    public IActionResult GetProjects() => Ok("Project list");

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [PermissionAuthorize("Project", "Read")]
    public IActionResult GetProject(int id) => Ok($"Project {id}");

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(403)]
    [PermissionAuthorize("Project", "Create")]
    public IActionResult CreateProject() => StatusCode(201, "Project created");

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [PermissionAuthorize("Project", "Update")]
    public IActionResult UpdateProject(int id) => Ok($"Project {id} updated");

    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [PermissionAuthorize("Project", "Delete")]
    public IActionResult DeleteProject(int id) => Ok($"Project {id} deleted");
}