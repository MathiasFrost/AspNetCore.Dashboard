namespace AspNetCore.APIUI.Models;

internal readonly struct LengthDefinition
{
	public int MaxLength { get; init; }

	public int MinLength { get; init; }
}