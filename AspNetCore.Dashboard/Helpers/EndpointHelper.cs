using System.Collections;
using AspNetCore.Dashboard.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AspNetCore.Dashboard.Helpers;

internal static class EndpointHelper
{
	public static IEnumerable<LengthDefinition> MapLengthDefinitions(this ApiParameterDescription description)
	{
		IEnumerable<LengthRouteConstraint>? constraints = description.RouteInfo?.Constraints
			?.OfType<LengthRouteConstraint>();

		constraints ??= Enumerable.Empty<LengthRouteConstraint>();
		return from constraint in constraints
			select new LengthDefinition { MaxLength = constraint.MaxLength, MinLength = constraint.MinLength };
	}

	public static TypeDefinition ToTypeDefinition(this Type? type)
	{
		TypeCode typeCode = type switch
		{
			null => TypeCode.Empty,
			_ when type.IsAssignableTo(typeof(sbyte)) => TypeCode.SByte,
			_ when type.IsAssignableTo(typeof(byte)) => TypeCode.Byte,
			_ when type.IsAssignableTo(typeof(short)) => TypeCode.Int16,
			_ when type.IsAssignableTo(typeof(ushort)) => TypeCode.UInt16,
			_ when type.IsAssignableTo(typeof(int)) => TypeCode.Int32,
			_ when type.IsAssignableTo(typeof(uint)) => TypeCode.UInt32,
			_ when type.IsAssignableTo(typeof(long)) => TypeCode.Int64,
			_ when type.IsAssignableTo(typeof(ulong)) => TypeCode.UInt64,
			_ when type.IsAssignableTo(typeof(float)) => TypeCode.Single,
			_ when type.IsAssignableTo(typeof(double)) => TypeCode.Double,
			_ when type.IsAssignableTo(typeof(decimal)) => TypeCode.Decimal,
			_ when type.IsAssignableTo(typeof(char)) => TypeCode.Char,
			_ when type.IsAssignableTo(typeof(string)) => TypeCode.String,
			_ when type.IsAssignableTo(typeof(bool)) => TypeCode.Boolean,
			_ when type.IsAssignableTo(typeof(DateTime)) => TypeCode.DateTime,
			_ when type.IsAssignableTo(typeof(DBNull)) => TypeCode.DBNull,
			_ => TypeCode.Object
		};

		switch (type)
		{
			case { } when type.IsAssignableTo(typeof(IEnumerable)) || type.IsArray:
			{
				Type? genericType = type.GenericTypeArguments.FirstOrDefault();
				return new TypeDefinition
				{
					TypeCode = typeCode,
					IsArray = true,
					IsEnum = false,
					Properties = genericType == null
						? Enumerable.Empty<TypeDefinition>()
						: new[] { genericType.ToTypeDefinition() },
					ValidValues = Enumerable.Empty<object?>()
				};
			}
			case { } when typeCode is TypeCode.Object:
				IEnumerable<Type> props = type.GetProperties().Select(info => info.PropertyType);
				return new TypeDefinition
				{
					TypeCode = typeCode,
					IsArray = false,
					IsEnum = true,
					Properties = from prop in props select prop.ToTypeDefinition(),
					ValidValues = Enumerable.Empty<object?>()
				};
			case { IsEnum: true }:
				return new TypeDefinition
				{
					TypeCode = typeCode,
					IsArray = false,
					IsEnum = true,
					Properties = Enumerable.Empty<TypeDefinition>(),
					ValidValues = Enumerable.Empty<object?>()
				};
			default:
				return new TypeDefinition
				{
					TypeCode = typeCode,
					IsArray = type?.IsArray ?? false,
					IsEnum = type?.IsEnum ?? false,
					Properties = Enumerable.Empty<TypeDefinition>(),
					ValidValues = Enumerable.Empty<object?>()
				};
		}
	}
}