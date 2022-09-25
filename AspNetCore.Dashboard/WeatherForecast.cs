using System.Diagnostics.CodeAnalysis;

namespace AspNetCore.Dashboard;

[SuppressMessage("ReSharper", "UnusedMember.Global"), SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global"),
 SuppressMessage("ReSharper", "MemberCanBeInternal")]
public sealed class WeatherForecast
{
	public DateTime Date { get; set; }

	public int TemperatureC { get; init; }

	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

	public string? Summary { get; set; }
}