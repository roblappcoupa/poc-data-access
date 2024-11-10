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
    private readonly IPersonRepositorySelector repositorySelector;

    public PersonService(IPersonRepositorySelector repositorySelector)
    {
        this.repositorySelector = repositorySelector;
    }

    public Task<Person> Create(CreatePerson person)
    {
        var repository = this.repositorySelector.GetRepositoryOrThrow();

        var result = repository.Create(
            new Person
            {
                PersonId = Guid.NewGuid(),
                Name = person.Name,
                Birthday = person.Birthday,
                Details = person.Details,
                CreatedOn = DateTime.UtcNow
            });

        return result;
    }

    public Task<Person> Get(Guid personId)
    {
        var repository = this.repositorySelector.GetRepositoryOrThrow();

        return repository.Get(personId);
    }

    public Task<IEnumerable<Person>> Get()
    {
        var repository = this.repositorySelector.GetRepositoryOrThrow();

        return repository.Get();
    }
}