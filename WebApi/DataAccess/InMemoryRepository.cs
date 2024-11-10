namespace WebApi.DataAccess;

using System.Collections.Concurrent;
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

    public Task<Person> Create(Person person)
    {
        Store[person.PersonId] = person;

        return person.WrapInTask();
    }

    public Task<Person> Get(Guid personId) => Store.GetValueOrDefault(personId).WrapInTask();

    public Task<IEnumerable<Person>> Get() => Store.Values.AsEnumerable().WrapInTask();
}