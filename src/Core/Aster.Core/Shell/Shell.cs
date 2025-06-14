using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aster.Core.Shell;

/// <summary>
/// Represents a runtime shell which wraps an <see cref="IServiceCollection"/> and
/// builds a service provider from it. Services can be updated at runtime and a
/// new provider will be created.
/// </summary>
public class Shell : IDisposable
{
    private IServiceProvider _serviceProvider;

    public Shell(IServiceCollection services)
    {
        Services = services;
        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Gets the service collection used by this shell.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Gets the current service provider.
    /// </summary>
    public IServiceProvider ServiceProvider => _serviceProvider;

    /// <summary>
    /// Rebuilds the service provider after applying the specified configuration
    /// to the service collection.
    /// </summary>
    public void Update(Action<IServiceCollection>? configure = null)
    {
        configure?.Invoke(Services);
        if (_serviceProvider is IDisposable d)
        {
            d.Dispose();
        }
        _serviceProvider = Services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (_serviceProvider is IDisposable d)
        {
            d.Dispose();
        }
    }
}
