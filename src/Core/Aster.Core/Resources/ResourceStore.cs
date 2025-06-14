using System;
using YesSql;
using YesSql.Provider.Sqlite;

namespace Aster.Core.Resources;

public class ResourceStore : IDisposable
{
    public IStore Store { get; }

    public ResourceStore(string connectionString)
    {
        var configuration = new YesSql.Configuration().UseSqLite(connectionString);
        Store = StoreFactory.CreateAndInitializeAsync(configuration).GetAwaiter().GetResult();
        Store.InitializeCollectionAsync(nameof(ResourceItem)).GetAwaiter().GetResult();
        Store.InitializeCollectionAsync(nameof(ResourceTypeDefinition)).GetAwaiter().GetResult();
    }

    public ISession CreateSession() => Store.CreateSession();

    public void Dispose()
    {
        (Store as IDisposable)?.Dispose();
    }
}
