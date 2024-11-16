namespace WebApi.DataAccess.MySql;

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
    
    public Task<Person> Create(Person person, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<Person> Get(Guid personId, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<IEnumerable<Person>> List(SearchParams searchParams, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}