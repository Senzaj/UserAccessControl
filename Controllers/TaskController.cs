using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Services;

namespace UserManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController: ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    [PermissionAuthorize("Task", "Read")]
    public IActionResult GetTasks() => Ok("Task list");

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [PermissionAuthorize("Task", "Read")]
    public IActionResult GetTask(int id) => Ok($"Task {id}");

    [HttpPost]
    [ProducesResponseType(201)]
    [PermissionAuthorize("Task", "Create")]
    public IActionResult CreateTask() => StatusCode(201, "Task created");

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [PermissionAuthorize("Task", "Update")]
    public IActionResult UpdateTask(int id) => Ok($"Task {id} updated");

    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [PermissionAuthorize("Task", "Delete")]
    public IActionResult DeleteTask(int id) => Ok($"Task {id} deleted");
}