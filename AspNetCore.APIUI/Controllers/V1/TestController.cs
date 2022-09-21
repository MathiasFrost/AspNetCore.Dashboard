using AspNetCore.APIUI.Helpers;
using AspNetCore.APIUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetCore.APIUI.Controllers.V1;

[ApiController, Route("V1/[controller]/[action]")]
public sealed class TestController : ControllerBase
{
	private readonly IApiDescriptionGroupCollectionProvider _provider;

	public TestController(IApiDescriptionGroupCollectionProvider provider) => _provider = provider;

	[HttpGet("{someParam:length(1,10)}")]
	public string Normal(string someParam, [FromQuery] string someQuery)
	{
		if (someParam == "Fail")
		{
			Response.StatusCode = StatusCodes.Status400BadRequest;
			return "No";
		}

		return "Heyhey";
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
					TypeDefinition = response?.Type.ToTypeDefinition() ?? new TypeDefinition(),
					MimeTypes = from format in response?.ApiResponseFormats
							?? Enumerable.Empty<ApiResponseFormat>()
						select format.MediaType
				}
			});
}