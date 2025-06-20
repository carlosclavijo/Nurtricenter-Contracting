using Contracting.Infrastructure.Extensions;
using Contracting.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Moq;
using Nur.Store2025.Observability.Config;

namespace Contracting.Test.Infrastructure.Extensions;

public class ObservabilityExtensionsTest
{
	[Fact]
	public void AddObservabilityIsValid()
	{
		var services = new ServiceCollection();
		var jaegerSettings = new JeagerSettings
		{
			Host = "localhost",
			Port = 6831,
		};
		var databaseSettings = new DatabaseSettings
		{
			ConnectionString = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=pass"
		};

		services.AddSingleton(jaegerSettings);
		services.AddSingleton(databaseSettings);
		services.AddLogging();

		var envMock = new Mock<IWebHostEnvironment>();
		IHostEnvironment environment = envMock.Object;

		services.AddHealthChecks();
		services.AddOpenTelemetry();
		services.AddObservability(environment, "MyService");

		var provider = services.BuildServiceProvider();
		var healthCheckService = provider.GetService<HealthCheckService>();

		Assert.NotNull(healthCheckService);
	}

	[Fact]
	public void AddServicesHealthChecksIsValid()
	{
		var services = new ServiceCollection();
		var databaseSettings = new DatabaseSettings
		{
			ConnectionString = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=pass"
		};

		services.AddSingleton(databaseSettings);
		services.AddLogging();
		services.AddServicesHealthChecks();

		var provider = services.BuildServiceProvider();
		var healthCheckService = provider.GetService<HealthCheckService>();

		Assert.NotNull(healthCheckService);
	}
}
