namespace WebApi.DataAccess;

using System.Collections.Concurrent;
using WebApi.Models;
using WebApi.Utils;

internal sealed class InMemoryRepository : IPersonRepository
{
    private static readonly ConcurrentDictionary<Guid, Person> Store = new();

    public Task<Person> Create(Person person)
    {
        Store[person.PersonId] = person;

        return person.WrapInTask();
    }

    public Task<Person> Get(Guid personId) => Store.GetValueOrDefault(personId).WrapInTask();

    public Task<IEnumerable<Person>> Get() => Store.Values.AsEnumerable().WrapInTask();
}