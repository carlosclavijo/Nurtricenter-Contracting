using Joseco.Communication.External.RabbitMQ;
using Joseco.Communication.External.RabbitMQ.Services;
using Microsoft.Extensions.DependencyInjection;
using Nur.Store2025.Integration.Catalog;
using Nur.Store2025.Integration.Identity;

namespace Contracting.Infrastructure.Extensions;

public static class BrokerExtensions
{
	public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
	{
		using var serviceProvider = services.BuildServiceProvider();
		var rabbitMqSettings = serviceProvider.GetRequiredService<RabbitMqSettings>();

		services.AddRabbitMQ(rabbitMqSettings);

		return services;
	}
}

