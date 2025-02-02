using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Contracting.Infrastructure.StoredModel;
using Contracting.Infrastructure.DomainModel;
using Contracting.Domain.Administrators;
using Contracting.Domain.Patients;
using Contracting.Domain.Contracts;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.Repositories;
using Contracting.Application;

namespace Contracting.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<StoredDbContext>(context => context.UseNpgsql(connectionString));
        services.AddDbContext<DomainDbContext>(context => context.UseNpgsql(connectionString));

        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddApplication();
        return services;
    }
}
