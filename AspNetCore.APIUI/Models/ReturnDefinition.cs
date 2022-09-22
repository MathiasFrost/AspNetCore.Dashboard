using System.Diagnostics.CodeAnalysis;

namespace AspNetCore.APIUI.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
internal readonly struct ReturnDefinition
{
	/// <summary> Determines JavaScript type </summary>
	public TypeDefinition TypeDefinition { get; init; }

	/// <summary> </summary>
	public IEnumerable<string> MimeTypes { get; init; }
}