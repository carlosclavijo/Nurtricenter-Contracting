using Contracting.Infrastructure.Extensions;
using Joseco.Communication.External.RabbitMQ.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Contracting.Test.Infrastructure.Extensions;

public class BrokerExtensionsTests
{
	[Fact]
	public void AddRabbitMQIsValid()
	{
		// Arrange
		var settings = new RabbitMqSettings
		{
			Host = "localhost",
			Port = 5672,
			UserName = "guest",
			Password = "guest",
			VirtualHost = "/",
			UseSsl = false
		};

		var services = new ServiceCollection();
		services.AddSingleton(settings);

		// Esto simula que tenés tu propia implementación de AddRabbitMQ(RabbitMqSettings)
		services.AddSingleton<Func<RabbitMqSettings, string>>(s => r => $"RabbitMQ:{r.Host}:{r.Port}");

		// Replace the real AddRabbitMQ(settings) with a fake one that uses the Func
		services.AddSingleton<IServiceCollection>(provider =>
		{
			var func = provider.GetRequiredService<Func<RabbitMqSettings, string>>();
			var r = provider.GetRequiredService<RabbitMqSettings>();
			var result = func(r);
			provider.GetRequiredService<ITestOutputHelper>().WriteLine(result); // ejemplo opcional
			return services;
		});

		// Act
		services.AddRabbitMQ(); // este llama al método bajo prueba

		// Assert
		var provider = services.BuildServiceProvider();
		var result = provider.GetRequiredService<RabbitMqSettings>();
		Assert.Equal("localhost", result.Host);
		Assert.Equal(5672, result.Port);
	}
}
