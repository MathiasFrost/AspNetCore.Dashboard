using Microsoft.AspNetCore.Mvc.ApiExplorer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<DefaultApiDescriptionProvider>();
builder.Services.AddCors(static options => options.AddDefaultPolicy(static policyBuilder =>
{
	policyBuilder.WithOrigins("http://localhost:5173");
	policyBuilder.AllowAnyMethod();
	policyBuilder.AllowAnyHeader();
}));

WebApplication app = builder.Build();

app.UseHttpLogging();

app.UseCors();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
