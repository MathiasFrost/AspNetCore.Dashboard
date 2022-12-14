using JetBrains.Annotations;

namespace AspNetCore.Dashboard.Models;

/// <summary> The model we send to the UI </summary>
[PublicAPI]
public readonly struct EndpointDefinition
{
	/// <summary> Shown in UI </summary>
	public string? RelativePath { get; init; }

	/// <summary> Shown in UI </summary>
	public string? HTTPMethod { get; init; }

	/// <summary> Endpoints in the Web API </summary>
	public IEnumerable<ParameterDefinition> Parameters { get; init; }

	public ReturnDefinition ReturnsDefinition { get; init; }
}
