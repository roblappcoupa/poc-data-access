namespace WebApi.Controllers;

using System.Net;
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

    [HttpGet("~/health")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult HealthCheck() => this.Ok(value: "Healthy");

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public Task<IEnumerable<Person>> List(
        [FromQuery] SearchParams searchParams,
        CancellationToken cancellationToken)
        => this.personService.List(searchParams, cancellationToken);

    [HttpGet("{personId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public Task<Person> Get(
        [FromRoute] Guid personId,
        CancellationToken cancellationToken)
        => this.personService.Get(personId, cancellationToken);

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreatePerson(
        [FromBody] CreatePerson createPerson,
        CancellationToken cancellationToken)
    {
        var person = await this.personService.Create(createPerson, cancellationToken);

        return this.CreatedAtAction(
            nameof(this.Get),
            new
            {
                personId = person.PersonId
            },
            person);
    }
}