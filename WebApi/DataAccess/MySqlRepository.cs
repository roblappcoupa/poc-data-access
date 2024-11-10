namespace WebApi.DataAccess;

using Microsoft.Extensions.Options;
using WebApi.Configuration;
using WebApi.Models;

internal sealed class MySqlRepository : RepositoryBase, IPersonRepository
{
    public MySqlRepository(
        IOptionsMonitor<ApplicationConfiguration> options)
        : base(options)
    {
    }

    public DataAccessProvider Provider => DataAccessProvider.MySql;
    
    public Task<Person> Create(Person person) => throw new NotImplementedException();

    public Task<Person> Get(Guid personId) => throw new NotImplementedException();

    public Task<IEnumerable<Person>> Get() => throw new NotImplementedException();
}