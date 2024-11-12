namespace WebApi.DependencyInjection;

using WebApi.DataAccess;
using WebApi.DataAccess.Cassandra;
using WebApi.DataAccess.InMemory;
using WebApi.DataAccess.MongoDb;
using WebApi.DataAccess.MySql;
using WebApi.DataAccess.SqlServer;
using WebApi.Services;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonTypes(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPersonRepositorySelector, PersonRepositorySelector>();
        serviceCollection.AddScoped<IPersonService, PersonService>();
        serviceCollection.AddSingleton<IPersonRepository, InMemoryRepository>(); // Singleton since it uses in an in-memory store
        serviceCollection.AddScoped<IPersonRepository, CassandraRepository>();
        serviceCollection.AddScoped<IPersonRepository, SqlServerRepository>();
        serviceCollection.AddScoped<IPersonRepository, MongoDbRepository>();
        serviceCollection.AddScoped<IPersonRepository, MySqlRepository>();
        
        return serviceCollection;
    }
}