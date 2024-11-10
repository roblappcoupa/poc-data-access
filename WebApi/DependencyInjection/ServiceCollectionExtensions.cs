namespace WebApi.DependencyInjection;

using WebApi.DataAccess;
using WebApi.Services;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonTypes(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPersonRepositorySelector, PersonRepositorySelector>();
        serviceCollection.AddSingleton<IPersonService, PersonService>();
        serviceCollection.AddSingleton<IPersonRepository, InMemoryRepository>();
        serviceCollection.AddSingleton<IPersonRepository, CassandraRepository>();
        serviceCollection.AddSingleton<IPersonRepository, SqlServerRepository>();
        serviceCollection.AddSingleton<IPersonRepository, MongoDbRepository>();
        serviceCollection.AddSingleton<IPersonRepository, MySqlRepository>();
        
        return serviceCollection;
    }
}