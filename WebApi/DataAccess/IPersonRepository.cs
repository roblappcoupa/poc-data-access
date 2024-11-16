namespace WebApi.DataAccess;

using WebApi.Configuration;
using WebApi.Models;

public interface IPersonRepository
{
    DataAccessProvider Provider { get; }
    
    Task<Person> Create(Person person, CancellationToken cancellationToken);
    
    Task<Person> Get(Guid personId, CancellationToken cancellationToken);

    Task<IEnumerable<Person>> List(SearchParams searchParams, CancellationToken cancellationToken);
}