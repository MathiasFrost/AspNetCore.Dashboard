using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetCore.APIUI.Controllers.V1;

[ApiController, Route("V1/[controller]/[action]")]
public sealed class TestController : ControllerBase
{
	private readonly IApiDescriptionGroupCollectionProvider _provider;

	public TestController(IApiDescriptionGroupCollectionProvider provider) => _provider = provider;

	[HttpGet("{someParam:length(1,10)}")]
	public string Normal(string someParam, [FromQuery] string someQuery) => "Heyhey";

	[HttpGet]
	public IEnumerable<Endpoint> Endpoints() =>
		from item in _provider.ApiDescriptionGroups.Items.SelectMany(@group => @group.Items)
		select new Endpoint
		{
			RelativePath = item.RelativePath,
			HTTPMethod = item.HttpMethod,
			Parameters = from param in item.ParameterDescriptions
				select new Endpoint.Parameter
				{
					Type = param.Type.FullName,
					Name = param.Name,
					IsRequired = param.IsRequired,
					DefaultValue = param.DefaultValue
				}
		};
}

/// <summary> The model we send to the UI </summary>
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global"), SuppressMessage("ReSharper", "MemberCanBeInternal")]
public readonly struct Endpoint
{
	/// <summary> Shown in UI </summary>
	public string? RelativePath { get; init; }

	/// <summary> Shown in UI </summary>
	public string? HTTPMethod { get; init; }

	public readonly struct Parameter
	{
		/// <summary> Label in UI </summary>
		public string Name { get; init; }

		/// <summary> Determines JavaScript type </summary>
		public string? Type { get; init; }

		/// <summary> Determines required fields in UI </summary>
		public bool IsRequired { get; init; }

		/// <summary> Shown in UI as a placeholder </summary>
		public object? DefaultValue { get; init; }
	}

	/// <summary> Endpoints in the Web API </summary>
	public IEnumerable<Parameter> Parameters { get; init; }
}