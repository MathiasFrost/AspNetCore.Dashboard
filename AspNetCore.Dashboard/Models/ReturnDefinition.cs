using JetBrains.Annotations;

namespace AspNetCore.Dashboard.Models;

[PublicAPI]
public readonly struct ReturnDefinition
{
	/// <summary> Determines JavaScript type </summary>
	public TypeDefinition TypeDefinition { get; init; }

	/// <summary> </summary>
	public IEnumerable<string> MimeTypes { get; init; }
}
