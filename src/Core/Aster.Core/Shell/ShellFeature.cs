using System;

namespace Aster.Core.Shell;

/// <summary>
/// Represents a feature that can be enabled within a shell.
/// </summary>
public class ShellFeature
{
    public required string Name { get; init; }

    /// <summary>
    /// The startup type that registers the services for this feature.
    /// </summary>
    public required Type StartupType { get; init; }
}
