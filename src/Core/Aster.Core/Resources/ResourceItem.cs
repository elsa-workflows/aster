using System.Collections.Generic;

namespace Aster.Core.Resources;

public class ResourceItem
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public IDictionary<string, object?> Aspects { get; } = new Dictionary<string, object?>();
}
