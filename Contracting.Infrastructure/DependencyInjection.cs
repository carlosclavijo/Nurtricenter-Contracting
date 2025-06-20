using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Contracting.Infrastructure.Extensions;

namespace Contracting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, string serviceName)
    {
		services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddSecrets(configuration, environment)
			.AddObservability(environment, serviceName)
			.AddDatabase()
			.AddSecurity(environment)
			.AddRabbitMQ();
			

		return services;
    }
}
