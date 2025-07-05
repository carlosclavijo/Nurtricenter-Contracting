using Contracting.Application;
using Contracting.Infrastructure;
using Contracting.WebApi;
using Contracting.WebApi.Extensions;
using Nur.Store2025.Observability;

var builder = WebApplication.CreateBuilder(args);

string serviceName = "contracting.api";

builder.Host.UseLogging(serviceName, builder.Configuration);

// Add services to the container.
builder.Services
	.AddApplication()
	.AddInfrastructure(builder.Configuration, builder.Environment, serviceName)
	.AddPresentation(builder.Configuration, builder.Environment);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy.WithOrigins("http://localhost:5173") // frontend Vite
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwaggerWithUi();
	//app.ApplyMigrations();
}

app.UseRouting();

app.UseHealthChecks();

app.UseRequestCorrelationId();

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
