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

