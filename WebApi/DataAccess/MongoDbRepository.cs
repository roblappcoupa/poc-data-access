namespace WebApi.DataAccess;

using Microsoft.Extensions.Options;
using WebApi.Configuration;
using WebApi.Models;

internal sealed class MongoDbRepository : RepositoryBase, IPersonRepository
{
    public MongoDbRepository(
        IOptionsMonitor<ApplicationConfiguration> options)
        : base(options)
    {
    }

    public DataAccessProvider Provider => DataAccessProvider.MongoDb;
    
    public Task<Person> Create(Person person) => throw new NotImplementedException();

    public Task<Person> Get(Guid personId) => throw new NotImplementedException();

    public Task<IEnumerable<Person>> Get() => throw new NotImplementedException();
}