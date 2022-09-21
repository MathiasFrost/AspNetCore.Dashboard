namespace AspNetCore.APIUI.Models;

internal readonly struct ReturnDefinition
{
	/// <summary> Determines JavaScript type </summary>
	public TypeDefinition TypeDefinition { get; init; }

	/// <summary> </summary>
	public IEnumerable<string> MimeTypes { get; init; }
}