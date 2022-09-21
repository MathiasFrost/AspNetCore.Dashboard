namespace AspNetCore.APIUI.Models;

public readonly struct TypeDefinition
{
	public TypeDefinition()
	{
		TypeCode = TypeCode.Object;
		IsArray = false;
		IsEnum = false;
		Properties = Enumerable.Empty<TypeDefinition>();
		ValidValues = Enumerable.Empty<object?>();
	}

	public TypeCode TypeCode { get; init; }

	public bool IsArray { get; init; }

	public bool IsEnum { get; init; }

	public IEnumerable<object?> ValidValues { get; init; }

	public IEnumerable<TypeDefinition> Properties { get; init; }
}