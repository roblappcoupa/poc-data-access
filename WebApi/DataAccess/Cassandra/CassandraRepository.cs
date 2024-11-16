namespace WebApi.DataAccess.Cassandra;

using global::Cassandra.Mapping;
using Microsoft.Extensions.Options;
using WebApi.Configuration;
using WebApi.Models;

internal sealed class CassandraRepository : RepositoryBase, IPersonRepository
{
    private readonly IMapper mapper;
    
    public CassandraRepository(
        IMapper mapper,
        IOptionsMonitor<ApplicationConfiguration> options)
        : base(options)
    {
        this.mapper = mapper;
    }

    public DataAccessProvider Provider => DataAccessProvider.Cassandra;

    public async Task<Person> Create(Person person, CancellationToken cancellationToken)
    {
        await this.mapper.InsertAsync(person);
        return person;
    }

    public async Task<Person> Get(Guid personId, CancellationToken cancellationToken)
    {
        var person = await this.mapper.FirstOrDefaultAsync<Person>("WHERE PersonId = ?", personId);

        return person;
    }

    public async Task<IEnumerable<Person>> List(SearchParams searchParams, CancellationToken cancellationToken)
    {
        var cql = CreateCassandraQuery(searchParams);

        var personList = await this.mapper.FetchAsync<Person>(cql);

        return personList;
    }

    private static string CreateCassandraQuery(SearchParams searchParams)
    {
        var cql = "SELECT * FROM application_keyspace.Person";
        
        if (!string.IsNullOrEmpty(searchParams.Filter))
        {
            cql += $" WHERE {searchParams.Filter}";
        }
        
        if (!string.IsNullOrEmpty(searchParams.OrderBy))
        {
            cql += $" ORDER BY {searchParams.OrderBy}";
        }
        
        if (searchParams.Page > 0 && searchParams.PageSize > 0)
        {
            cql += $" LIMIT {searchParams.PageSize}";
        }

        return cql;
    }
}