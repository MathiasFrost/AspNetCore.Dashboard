using System.Diagnostics.CodeAnalysis;

namespace AspNetCore.APIUI.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
internal readonly struct LengthDefinition
{
	public int MaxLength { get; init; }

	public int MinLength { get; init; }
}