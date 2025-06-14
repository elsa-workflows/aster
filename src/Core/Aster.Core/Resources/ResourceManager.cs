using System.Threading.Tasks;
using System.Linq;
using YesSql;

namespace Aster.Core.Resources;

public class ResourceManager
{
    private readonly ResourceStore _store;

    public ResourceManager(ResourceStore store)
    {
        _store = store;
    }

    public async Task SaveTypeAsync(ResourceTypeDefinition type)
    {
        using var session = _store.CreateSession();
        session.Save(type, collection: nameof(ResourceTypeDefinition));
        await session.SaveChangesAsync();
    }

    public async Task<ResourceTypeDefinition?> GetTypeAsync(string name)
    {
        using var session = _store.CreateSession();
        var all = await session.Query<ResourceTypeDefinition>(collection: nameof(ResourceTypeDefinition))
            .ListAsync();
        return all.FirstOrDefault(x => x.Name == name);
    }

    public async Task<int> CreateItemAsync(ResourceItem item)
    {
        using var session = _store.CreateSession();
        session.Save(item, collection: nameof(ResourceItem));
        await session.SaveChangesAsync();
        return item.Id;
    }

    public async Task<ResourceItem?> GetItemAsync(int id)
    {
        using var session = _store.CreateSession();
        return await session.GetAsync<ResourceItem>(id, collection: nameof(ResourceItem));
    }
}
