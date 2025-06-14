using Microsoft.Extensions.DependencyInjection;

namespace Aster.Core.Shell;

/// <summary>
/// Implemented by modules to register services for a feature.
/// </summary>
public interface IFeatureStartup
{
    void ConfigureServices(IServiceCollection services);
}
