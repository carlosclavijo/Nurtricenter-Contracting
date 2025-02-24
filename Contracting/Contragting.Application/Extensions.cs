using System;
using System.Reflection;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Microsoft.Extensions.DependencyInjection;

namespace Contracting.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()) );

        services.AddSingleton<IAdministratorFactory, AdministratorFactory>();
        services.AddSingleton<IPatienteFactory, PatientFactory>();
        services.AddSingleton<IContractFactory, ContractFactory>();

        return services;
    }
}
