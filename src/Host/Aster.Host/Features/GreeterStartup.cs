using Aster.Core.Shell;
using Microsoft.Extensions.DependencyInjection;

namespace Aster.Host.Features;

public class GreeterStartup : IFeatureStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGreeter, Greeter>();
    }
}

public interface IGreeter
{
    string Greet();
}

public class Greeter : IGreeter
{
    public string Greet() => "Hello from shell";
}
