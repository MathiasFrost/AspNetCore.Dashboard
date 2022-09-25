using AspNetCore.Dashboard.Helpers;
using AspNetCore.Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetCore.Dashboard.Controllers.V1;

[ApiController, Route("V1/[controller]/[action]")]
public sealed class TestController : ControllerBase
{
	private readonly IApiDescriptionGroupCollectionProvider _provider;

	public TestController(IApiDescriptionGroupCollectionProvider provider) => _provider = provider;

	[HttpGet("{someParam:length(1,10)}")]
	public string Normal(string someParam, [FromQuery] string someQuery)
	{
		if (someParam != "Fail")
		{
			return "Heyhey";
		}
		Response.StatusCode = StatusCodes.Status400BadRequest;
		return "No";
	}

	[HttpGet] public IEnumerable<string> Array() => new[] { "Wee", "woo" };

	[HttpGet]
	public IActionResult Endpoints() =>
		Ok(from item in _provider.ApiDescriptionGroups.Items.SelectMany(@group => @group.Items)
			where item.RelativePath != "V1/Test/Endpoints" && item.HttpMethod == HttpMethods.Get
			let response = item.SupportedResponseTypes.FirstOrDefault()
			select new EndpointDefinition
			{
				RelativePath = item.RelativePath,
				HTTPMethod = item.HttpMethod,
				Parameters = from param in item.ParameterDescriptions
					select new ParameterDefinition
					{
						TypeDefinition = param.Type.ToTypeDefinition(),
						Name = param.Name,
						IsRequired = param.IsRequired,
						DefaultValue = param.DefaultValue,
						BindingSource = param.Source.Id,
						LengthConstraints = param.MapLengthDefinitions()
					},
				ReturnsDefinition = new ReturnDefinition
				{
					TypeDefinition = response?.Type?.ToTypeDefinition() ?? new TypeDefinition(),
					MimeTypes = response?.ApiResponseFormats?.Select(format => format.MediaType)
				}
			});
}