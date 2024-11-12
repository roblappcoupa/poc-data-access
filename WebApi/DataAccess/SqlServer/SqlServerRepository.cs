namespace WebApi.DataAccess.SqlServer;

using Gridify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Configuration;
using WebApi.Models;
using WebApi.Utils;

internal sealed class SqlServerRepository : RepositoryBase, IPersonRepository
{
    private readonly ApplicationDbContext context;
    
    public SqlServerRepository(
        IOptionsMonitor<ApplicationConfiguration> options,
        ApplicationDbContext context)
        : base(options)
    {
        this.context = context;
    }

    public DataAccessProvider Provider => DataAccessProvider.SqlServer;

    public async Task<Person> Create(Person person)
    {
        await this.context.Persons.AddAsync(person);

        await this.context.SaveChangesAsync();

        return person;
    }

    public async Task<Person> Get(Guid personId)
    {
        var person = await this.context.Persons.FirstOrDefaultAsync(x => x.PersonId == personId);

        return person;
    }

    public async Task<IEnumerable<Person>> List(SearchParams searchParams)
    {
        var gridifyQuery = searchParams.ToGridifyQuery();
        
        var query = this.context.Persons
            .AsQueryable()
            .ApplyFilteringAndOrdering(gridifyQuery);
        
        var persons = await query.ToListAsync();

        return persons;
    }
}