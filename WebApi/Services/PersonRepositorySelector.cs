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
    private readonly ILogger<PersonRepositorySelector> logger;

    public PersonRepositorySelector(
        IEnumerable<IPersonRepository> personRepositories,
        IOptionsMonitor<ApplicationConfiguration> options,
        ILogger<PersonRepositorySelector> logger)
    {
        this.personRepositories = personRepositories.ToList();
        this.options = options;
        this.logger = logger;
    }

    public IPersonRepository GetRepositoryOrThrow()
    {
        var targetProvider = this.options.CurrentValue.Provider;

        this.logger.LogInformation("Target provider: {Provider}", targetProvider);

        var selectedRepository = this.personRepositories.FirstOrDefault(x => x.Provider == targetProvider);

        if (selectedRepository == null)
        {
            throw new ConfigurationException("Could not find an enabled repository");
        }

        this.logger.LogInformation("Selected provider: {Provider}", selectedRepository.Provider);

        return selectedRepository;
    }
}