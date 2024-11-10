namespace WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService personService;
    
    public PersonController(IPersonService personService)
    {
        this.personService = personService;
    }

    [HttpGet("health")]
    public IActionResult HealthCheck() => this.Ok(value: "Healthy");

    [HttpGet]
    public Task<IEnumerable<Person>> GetAll() => this.personService.Get();

    [HttpGet("{personId:guid}")]
    public Task<Person> Get([FromRoute] Guid personId) => this.personService.Get(personId);

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePerson createPerson)
    {
        var person = await this.personService.Create(createPerson);

        return this.CreatedAtAction(nameof(this.Get), new { person = person.PersonId }, person);
    }
}