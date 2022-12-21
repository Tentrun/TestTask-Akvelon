using Akvelon_web_api.Domain.Entity;
using Akvelon_web_api.Domain.Response;
using Akvelon_web_api.Service.Interfaces.BaseInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon_web_api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IGenericService<TaskEntity> _service;

    public TasksController(IGenericService<TaskEntity> service)
    {
        _service = service;
    }

    // GET : /Tasks/Get
    // Get all tasks
    [HttpGet("Get")]
    public async Task<IActionResult> GetTasksByProject(int projectId)
    {
        var response = await _service.Get();
        
        if (response.IsSuccessful)
        {
            var tasks = response.Data;
            return Ok(tasks?.Where(t => t.ProjectId == projectId));
        }
        return BadRequest(response.BrokenRules);
    }
    
    // GET : /Tasks/GetById
    // Get tasks with id
    [HttpGet("GetById")]
    public async Task<IActionResult> GetTask(int id)
    {
        var response = await _service.FindById(id);
        
        if (response.IsSuccessful)
        {
            return Ok(response.Data);
        }
        return BadRequest(response.BrokenRules);
    }
    
    // POST : /Tasks/Create
    // Create new task
    [HttpPost("Create")]
    public async Task<IActionResult> CreateTask(TaskEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ArgumentNullException());
        }
        var response = await _service.Create(entity);
        
        if (response.IsSuccessful)
        {
            return Created("Created" , response.Data);
        }
        return Conflict();
    }
    
    // POST : /Tasks/Edit
    // Edit already existing element from received model
    [HttpPost("Edit")]
    public async Task<IActionResult> EditTask(TaskEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ArgumentNullException());
        }
        
        var response = await _service.Update(entity.Id, entity);
        if (response.IsSuccessful)
        {
            return Ok(entity);
        }

        return BadRequest();
    }
    
    // DELETE : /Tasks/Delete
    // Removing element by received id
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteTask(int id)
    { 
        var response = await _service.Remove(id);
        if (response.IsSuccessful)
        {
            return Ok();
        }
        return BadRequest(response.BrokenRules);
    }
}