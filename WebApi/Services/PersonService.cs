namespace WebApi.Services;

using WebApi.Models;

public interface IPersonService
{
    Task<Person> Create(CreatePerson person);
    
    Task<Person> Get(Guid personId);

    Task<IEnumerable<Person>> Get();
}

internal sealed class PersonService : IPersonService
{
    public Task<Person> Create(CreatePerson person) => throw new NotImplementedException();

    public Task<Person> Get(Guid personId) => throw new NotImplementedException();

    public Task<IEnumerable<Person>> Get() => throw new NotImplementedException();
}