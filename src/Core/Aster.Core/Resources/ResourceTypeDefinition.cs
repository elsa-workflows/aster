using System.Collections.Generic;

namespace Aster.Core.Resources;

public class ResourceTypeDefinition
{
    public required string Name { get; set; }
    public IList<AspectDefinition> Aspects { get; } = new List<AspectDefinition>();
}
