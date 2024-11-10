namespace WebApi.DataAccess;

using Microsoft.Extensions.Options;
using WebApi.Configuration;

internal abstract class RepositoryBase
{
    protected RepositoryBase(IOptionsMonitor<ApplicationConfiguration> options)
    {
        this.Options = options;
    }

    protected IOptionsMonitor<ApplicationConfiguration> Options { get; }
}