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
		services.AddSingleton<Func<RabbitMqSettings, string>>(s => r => $"RabbitMQ:{r.Host}:{r.Port}");
		services.AddSingleton<IServiceCollection>(provider =>
		{
			var func = provider.GetRequiredService<Func<RabbitMqSettings, string>>();
			var r = provider.GetRequiredService<RabbitMqSettings>();
			var result = func(r);
			provider.GetRequiredService<ITestOutputHelper>().WriteLine(result);
			return services;
		});

		services.AddRabbitMQ();
		var provider = services.BuildServiceProvider();
		var result = provider.GetRequiredService<RabbitMqSettings>();
		Assert.Equal("localhost", result.Host);
		Assert.Equal(5672, result.Port);
	}
}
