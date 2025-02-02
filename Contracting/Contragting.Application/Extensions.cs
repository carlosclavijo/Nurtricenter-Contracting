using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Microsoft.Extensions.DependencyInjection;

namespace Contracting.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddSingleton<IAdministratorFactory, AdministradorFactory>();
        services.AddSingleton<IPatienteFactory, PatienteFactory>();
        services.AddSingleton<IContractFactory, ContractFactory>();

        return services;
    }
}
