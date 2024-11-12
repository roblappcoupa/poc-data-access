namespace WebApi.Services;

using WebApi.DataAccess;
using WebApi.Models;

public interface IPersonService
{
    Task<Person> Create(CreatePerson person);
    
    Task<Person> Get(Guid personId);

    Task<IEnumerable<Person>> List(SearchParams searchParams);
}

internal sealed class PersonService : IPersonService
{
    private readonly IPersonRepositorySelector repositorySelector;

    public PersonService(IPersonRepositorySelector repositorySelector)
    {
        this.repositorySelector = repositorySelector;
    }

    private IPersonRepository Repository => this.repositorySelector.GetRepositoryOrThrow();

    public Task<Person> Create(CreatePerson person)
        => this.Repository.Create(
            new Person
            {
                PersonId = Guid.NewGuid(),
                Name = person.Name,
                Birthday = person.Birthday,
                Details = person.Details,
                CreatedOn = DateTime.UtcNow
            });

    public Task<Person> Get(Guid personId) => this.Repository.Get(personId);

    public Task<IEnumerable<Person>> List(SearchParams searchParams) => this.Repository.List(searchParams);
}