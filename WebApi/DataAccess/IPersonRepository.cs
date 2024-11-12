namespace WebApi.DataAccess;

using WebApi.Configuration;
using WebApi.Models;

public interface IPersonRepository
{
    DataAccessProvider Provider { get; }
    
    Task<Person> Create(Person person);
    
    Task<Person> Get(Guid personId);

    Task<IEnumerable<Person>> List(SearchParams searchParams);
}