WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder =>
{
	policyBuilder.WithOrigins("http://localhost:4200");
	policyBuilder.AllowAnyMethod();
	policyBuilder.AllowAnyHeader();
}));

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();