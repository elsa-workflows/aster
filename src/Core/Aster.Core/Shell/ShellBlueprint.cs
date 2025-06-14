using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Aster.Core.Shell;

/// <summary>
/// Describes the modules and features loaded by a shell.
/// </summary>
public class ShellBlueprint
{
    public IList<ShellFeature> Features { get; } = new List<ShellFeature>();

    /// <summary>
    /// Applies the blueprint by invoking each feature's startup class to register
    /// its services into the collection.
    /// </summary>
    public void ApplyServices(IServiceCollection services)
    {
        foreach (var feature in Features)
        {
            if (Activator.CreateInstance(feature.StartupType) is IFeatureStartup startup)
            {
                startup.ConfigureServices(services);
            }
        }
    }

    /// <summary>
    /// Creates a blueprint from the specified JSON recipe.
    /// </summary>
    public static ShellBlueprint FromJson(string json)
    {
        var recipe = JsonSerializer.Deserialize<Recipe>(json) ?? new Recipe();
        var blueprint = new ShellBlueprint();
        foreach (var feat in recipe.Features)
        {
            var type = Type.GetType(feat.Startup) ??
                       throw new InvalidOperationException($"Startup type '{feat.Startup}' not found.");
            blueprint.Features.Add(new ShellFeature
            {
                Name = feat.Name,
                StartupType = type
            });
        }
        return blueprint;
    }

    private class Recipe
    {
        [JsonPropertyName("features")]
        public List<RecipeFeature> Features { get; set; } = new();
    }

    private class RecipeFeature
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("startup")]
        public string Startup { get; set; } = string.Empty;
    }
}
