namespace WebApi.Services;

using Microsoft.Extensions.Options;
using WebApi.Configuration;
using WebApi.DataAccess;
using WebApi.Exceptions;

public interface IPersonRepositorySelector
{
    IPersonRepository GetRepositoryOrThrow();
}

internal sealed class PersonRepositorySelector : IPersonRepositorySelector
{
    private readonly IList<IPersonRepository> personRepositories;
    private readonly IOptionsMonitor<ApplicationConfiguration> options;

    public PersonRepositorySelector(
        IEnumerable<IPersonRepository> personRepositories,
        IOptionsMonitor<ApplicationConfiguration> options)
    {
        this.personRepositories = personRepositories.ToList();
        this.options = options;
    }

    public IPersonRepository GetRepositoryOrThrow()
    {
        var targetProvider = this.options.CurrentValue.Provider;

        var selectedRepository = this.personRepositories.FirstOrDefault(x => x.Provider == targetProvider);

        if (selectedRepository == null)
        {
            throw new ConfigurationException("Could not find an enabled repository");
        }

        return selectedRepository;
    }
}