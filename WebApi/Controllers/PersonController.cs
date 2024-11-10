namespace WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult HealthCheck() => this.Ok(value: "Healthy");
    
    [HttpGet]
    public async Task<IEnumerable<dynamic>> GetAll()
    {
        
    }
    
    [HttpGet]
    public async Task<IEnumerable<dynamic>> Get([FromRoute]Guid personId)
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePerson person)
    {
        Guid id = Guid.NewGuid();
        var query = $"INSERT INTO people (id, name, age) VALUES ({id}, '{person.Name}', {person.Age})";
        await _session.ExecuteAsync(new SimpleStatement(query));

        return CreatedAtAction(nameof(this.Get), new { personId }, person);
    }
}