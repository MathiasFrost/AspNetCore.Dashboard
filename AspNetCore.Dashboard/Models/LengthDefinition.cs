using JetBrains.Annotations;

namespace AspNetCore.Dashboard.Models;

[PublicAPI]
public readonly struct LengthDefinition
{
	public int MaxLength { get; init; }

	public int MinLength { get; init; }
}
