using Aster.Core.Shell;
using Aster.Host.Features;

var builder = WebApplication.CreateBuilder(args);

// Load shell recipe and build blueprint
var recipePath = Path.Combine(AppContext.BaseDirectory, "Recipes", "default.json");
var recipeJson = File.ReadAllText(recipePath);
var blueprint = ShellBlueprint.FromJson(recipeJson);

// Create the shell and apply services
var shell = new Shell(new ServiceCollection());
blueprint.ApplyServices(shell.Services);
shell.Update();

// Register shell services with the host so that endpoints can resolve them
blueprint.ApplyServices(builder.Services);

var app = builder.Build();

app.MapGet("/", (IGreeter greeter) => greeter.Greet());

app.Run();
