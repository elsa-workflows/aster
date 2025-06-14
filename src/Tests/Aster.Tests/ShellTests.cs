using Aster.Core.Shell;
using Microsoft.Extensions.DependencyInjection;

namespace Aster.Tests;

public class ShellTests
{
    [Fact]
    public void ServiceProvider_Rebuilds_On_Update()
    {
        var services = new ServiceCollection();
        var shell = new Shell(services);

        shell.Update(s => s.AddSingleton<IMessage, MessageA>());
        var msg = shell.ServiceProvider.GetRequiredService<IMessage>();
        Assert.IsType<MessageA>(msg);

        shell.Update(s => {
            s.Remove(new ServiceDescriptor(typeof(IMessage), typeof(MessageA), ServiceLifetime.Singleton));
            s.AddSingleton<IMessage, MessageB>();
        });

        msg = shell.ServiceProvider.GetRequiredService<IMessage>();
        Assert.IsType<MessageB>(msg);
    }

    [Fact]
    public void Blueprint_Registers_Feature_Services()
    {
        var json = "{\"features\":[{\"name\":\"Test\",\"startup\":\"Aster.Tests.ShellTests+TestStartup, Aster.Tests\"}]}";
        var blueprint = ShellBlueprint.FromJson(json);
        var services = new ServiceCollection();
        blueprint.ApplyServices(services);
        var shell = new Shell(services);

        var msg = shell.ServiceProvider.GetRequiredService<IMessage>();
        Assert.IsType<MessageA>(msg);
    }

    private interface IMessage { }
    private class MessageA : IMessage { }
    private class MessageB : IMessage { }

    private class TestStartup : IFeatureStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessage, MessageA>();
        }
    }
}
