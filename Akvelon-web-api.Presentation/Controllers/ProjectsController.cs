using Akvelon_web_api.Domain.Entity;
using Akvelon_web_api.Domain.Response;
using Akvelon_web_api.Service.Interfaces.BaseInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon_web_api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IGenericService<ProjectEntity> _service;

    public ProjectsController(IGenericService<ProjectEntity> service)
    {
        _service = service;
    }

    // GET : /Project/Get
    // Get all projects
    [HttpGet("Get")]
    public async Task<IActionResult> GetProjects()
    {
        var response = await _service.Get();
        
        if (response.IsSuccessful)
        {
            return Ok(response.Data);
        }
        return BadRequest(response.BrokenRules);
    }
    
    // GET : /Project/GetById
    // Get project with id
    [HttpGet("GetById")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _service.FindById(id);
        
        if (project.IsSuccessful)
        {
            return Ok(project.Data);
        }
        return BadRequest(project.BrokenRules);
    }
    
    // POST : /Project/Create
    // Create new project
    [HttpPost("Create")]
    public async Task<IActionResult> CreateProject(ProjectEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ArgumentNullException());
        }

        if (entity.StartDate <= entity.CompleteDate)
        {
            return Conflict(ErrorsList.WrongDate);
        }
        
        var project = await _service.Create(entity);
        if (project.IsSuccessful)
        {
            return Created("Created" , project.Data);
        }
        return Conflict();
    }
    
    // POST : /Project/Edit
    // Edit already existing element from received model
    [HttpPost("Edit")]
    public async Task<IActionResult> EditProject(ProjectEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ArgumentNullException());
        }

        if (entity.StartDate <= entity.CompleteDate)
        {
            return Conflict(ErrorsList.WrongDate);
        }
        
        var response = await _service.Update(entity.Id, entity);
        if (response.IsSuccessful)
        {
            return Ok(entity);
        }

        return BadRequest();
    }
    
     [HttpDelete("Delete")]
     public async Task<IActionResult> DeleteProject(int id)
     {
         var response = await _service.Remove(id);
         if (response.IsSuccessful)
         {
             return Ok();
         }

         return BadRequest(response.BrokenRules);
     }
}