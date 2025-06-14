using System.Collections.Generic;

namespace Aster.Core.Resources;

public class AspectDefinition
{
    public required string Name { get; set; }
    public string? TypeName { get; set; }
    public IList<FacetDefinition> Facets { get; } = new List<FacetDefinition>();
}
