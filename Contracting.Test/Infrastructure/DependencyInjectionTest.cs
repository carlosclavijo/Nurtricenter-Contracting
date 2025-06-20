using Contracting.Infrastructure;
using Contracting.Infrastructure.Persistence;
using Joseco.Communication.External.RabbitMQ.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Nur.Store2025.Observability.Config;
using Nur.Store2025.Security.Config;

namespace Contracting.Test.Infrastructure;

public class DependencyInjectionTest
{
	[Fact]
	public void AddInfrastructure_ShouldRegisterExpectedServices()
	{
		var services = new ServiceCollection();

		var configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ "UseSecretManager", "false" }
            })
			.Build();

		var environmentMock = new Mock<IHostEnvironment>();
		environmentMock.Setup(e => e.EnvironmentName).Returns("Development");

		services.AddSingleton(new RabbitMqSettings
		{
			Host = "localhost",
			UserName = "test",
			Password = "test",
			VirtualHost = "/"
		});

		services.AddSingleton(new DatabaseSettings
		{
			ConnectionString = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=pass"
		});

		services.AddSingleton(new JeagerSettings
		{
			Host = "localhost",
			Port = 6831
		});

		services.AddSingleton(new JwtOptions
		{
			Lifetime = 60,
			SecretKey = "test-secret",
			ValidAudience = "test",
			ValidIssuer = "test",
			ValidateAudience = false,
			ValidateIssuer = false,
			ValidateLifetime = false
		});

		services.AddInfrastructure(configuration, environmentMock.Object, "test-service");

		var provider = services.BuildServiceProvider();

		// Assert
		Assert.NotNull(provider.GetService<RabbitMqSettings>());
		Assert.NotNull(provider.GetService<DatabaseSettings>());
		Assert.NotNull(provider.GetService<JwtOptions>());
		Assert.NotNull(provider.GetService<JeagerSettings>());
		Assert.NotNull(provider.GetService<IMediator>());
	}
}
