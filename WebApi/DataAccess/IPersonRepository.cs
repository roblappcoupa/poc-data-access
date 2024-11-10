namespace WebApi.DataAccess;

using WebApi.Models;

public interface IPersonRepository
{
    Task<Person> Create(Person person);
    
    Task<Person> Get(Guid personId);

    Task<IEnumerable<Person>> Get();
}