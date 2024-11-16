namespace WebApi.Services;

using WebApi.DataAccess;
using WebApi.Models;

public interface IPersonService
{
    Task<Person> Create(CreatePerson person, CancellationToken cancellationToken);
    
    Task<Person> Get(Guid personId, CancellationToken cancellationToken);

    Task<IEnumerable<Person>> List(SearchParams searchParams, CancellationToken cancellationToken);
}

internal sealed class PersonService : IPersonService
{
    private readonly IPersonRepositorySelector repositorySelector;

    public PersonService(IPersonRepositorySelector repositorySelector)
    {
        this.repositorySelector = repositorySelector;
    }

    private IPersonRepository Repository => this.repositorySelector.GetRepositoryOrThrow();

    public Task<Person> Create(CreatePerson person, CancellationToken cancellationToken)
        => this.Repository.Create(
            new Person
            {
                PersonId = Guid.NewGuid(),
                Name = person.Name,
                Birthday = person.Birthday,
                Details = person.Details,
                CreatedOn = DateTime.UtcNow
            },
            cancellationToken);

    public Task<Person> Get(Guid personId, CancellationToken cancellationToken)
        => this.Repository.Get(personId, cancellationToken);

    public Task<IEnumerable<Person>> List(SearchParams searchParams, CancellationToken cancellationToken)
        => this.Repository.List(searchParams, cancellationToken);
}