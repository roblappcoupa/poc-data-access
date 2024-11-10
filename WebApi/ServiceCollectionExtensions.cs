namespace WebApi;

using WebApi.Services;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonTypes(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPersonService, PersonService>();
        
        return serviceCollection;
    }
}