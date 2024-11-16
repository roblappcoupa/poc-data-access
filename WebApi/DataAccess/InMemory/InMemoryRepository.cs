namespace WebApi.DataAccess.InMemory;

using System.Collections.Concurrent;
using Gridify;
using Microsoft.Extensions.Options;
using WebApi.Configuration;
using WebApi.Models;
using WebApi.Utils;

internal sealed class InMemoryRepository : RepositoryBase, IPersonRepository
{
    private static readonly ConcurrentDictionary<Guid, Person> Store = new();
    
    public InMemoryRepository(
        IOptionsMonitor<ApplicationConfiguration> options)
        : base(options)
    {
    }

    public DataAccessProvider Provider => DataAccessProvider.InMemory;

    public Task<Person> Create(Person person, CancellationToken cancellationToken)
    {
        Store[person.PersonId] = person;

        return person.WrapInTask();
    }

    public Task<Person> Get(Guid personId, CancellationToken cancellationToken) => Store.GetValueOrDefault(personId).WrapInTask();

    public Task<IEnumerable<Person>> List(SearchParams searchParams, CancellationToken cancellationToken)
    {
        var gridifyQuery = searchParams.ToGridifyQuery();
        
        var result = Store.Values
            .AsQueryable()
            .ApplyFilteringAndOrdering(gridifyQuery)
            .AsEnumerable();

        return result.WrapInTask();
    }
}