namespace WebApi.DependencyInjection;

using Cassandra;
using Cassandra.Mapping;
using WebApi.Configuration;
using WebApi.DataAccess;
using WebApi.DataAccess.Cassandra;
using WebApi.DataAccess.InMemory;
using WebApi.DataAccess.SqlServer;
using WebApi.Models;
using WebApi.Services;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultPersonTypes(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPersonRepositorySelector, PersonRepositorySelector>();
        serviceCollection.AddScoped<IPersonService, PersonService>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddInMemoryDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPersonRepository, InMemoryRepository>(); // Singleton since it uses in an in-memory store
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddSqlServerDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPersonRepository, SqlServerRepository>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddCassandraDataAccess(
        this IServiceCollection serviceCollection,
        CassandraConfiguration cassandraConfiguration)
    {
        var cluster = Cluster.Builder()
            .AddContactPoint(cassandraConfiguration.ContactPoint)
            .Build();
        
        // We have to build a temporary service provider to get the ILoggerProvider
        using var serviceProvider = serviceCollection.BuildServiceProvider();
        var loggingProvider = serviceProvider.GetRequiredService<ILoggerProvider>();
        Diagnostics.AddLoggerProvider(new MyLoggerProvider(loggingProvider));

        var session = cluster.Connect();

        MappingConfiguration.Global.Define(
            new Map<Person>()
                .TableName("Person")
                .KeyspaceName("application_keyspace")
                .PartitionKey(p => p.PersonId));
        
        serviceCollection.AddSingleton(session);

        serviceCollection.AddSingleton<IMapper>(new Mapper(session));

        serviceCollection.AddScoped<IPersonRepository, CassandraRepository>();
        
        return serviceCollection;
    }

    private sealed class MyLoggerProvider : ILoggerProvider
    {
        private readonly ILoggerProvider innerProvider;

        public MyLoggerProvider(ILoggerProvider innerProvider)
        {
            this.innerProvider = innerProvider;
        }

        public ILogger CreateLogger(string categoryName) => this.innerProvider.CreateLogger(categoryName);
        
        public void Dispose()
        {
            Console.WriteLine("\n\nDISPOSE WAS CALLED\n\n");
        }
    }
}