# Aster

Aster is a modular ASP.NET Core application platform.

## Building

Make sure the .NET 9 SDK is installed. The repository uses an early preview of .NET 9. Use the included `dotnet-install.sh` script if needed.

To build all projects and run tests:

```bash
export PATH="$HOME/dotnet:$PATH"
dotnet build Aster.sln
```

Run the tests:

```bash
dotnet test Aster.sln
```

### Example: Defining a Page resource

The following snippet demonstrates how to define a `Page` resource type with a
`Title` aspect and then create a new resource item:

```csharp
using Aster.Core.Resources;

using var store = new ResourceStore("Data Source=aster.db");
var manager = new ResourceManager(store);

// Define the aspect and type
var titleAspect = new AspectDefinition
{
    Name = "Title",
    Facets = { new FacetDefinition { Name = "Text", Type = "string" } }
};

var pageType = new ResourceTypeDefinition
{
    Name = "Page",
    Aspects = { titleAspect }
};

await manager.SaveTypeAsync(pageType);

// Create a new Page item
var item = new ResourceItem { Type = "Page" };
item.Aspects["Title"] = new { Text = "Hello world" };
await manager.CreateItemAsync(item);
```

