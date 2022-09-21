namespace AspNetCore.APIUI.Models;

internal readonly struct ParameterDefinition
{
	/// <summary> Label in UI </summary>
	public string Name { get; init; }

	/// <summary> Determines JavaScript type </summary>
	public TypeDefinition TypeDefinition { get; init; }

	/// <summary> Determines required fields in UI </summary>
	public bool IsRequired { get; init; }

	/// <summary> Shown in UI as a placeholder </summary>
	public object? DefaultValue { get; init; }

	/// <summary> How this parameter is bound </summary>
	public string? BindingSource { get; init; }

	/// <summary> Constraints for the parameter </summary>
	public IEnumerable<LengthDefinition> LengthConstraints { get; init; }
}