using Consul;
using Contracting.WebApi.Infrastructure;

namespace Contracting.WebApi;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddControllers();
		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();

		if (!environment.IsDevelopment())
		{
			services.RegisterServiceToServiceDiscovery(configuration);
		}

		return services;
	}

	private static IServiceCollection RegisterServiceToServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
	{

		string? serviceDiscoveryAddress = configuration.GetValue<string?>("ServiceRegistration:ServiceDiscoveryAddress");

		services.AddSingleton(sp => new ConsulClient(c => c.Address = new Uri(serviceDiscoveryAddress)));
		services.AddHostedService<ServiceRegistration>();

		return services;
	}
}

