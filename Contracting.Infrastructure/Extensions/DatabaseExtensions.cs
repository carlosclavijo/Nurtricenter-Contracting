using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Contracting.Infrastructure.Persistence;
using Contracting.Infrastructure.Persistence.DomainModel;
using Contracting.Infrastructure.Persistence.Repositories;
using Contracting.Infrastructure.Persistence.StoredModel;
using Joseco.Outbox.Contracts.Service;
using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contracting.Infrastructure.Extensions;

public static class DatabaseExtensions
{
	public static IServiceCollection AddDatabase(this IServiceCollection services)
	{
		var databaseSettings = services.BuildServiceProvider().GetRequiredService<DatabaseSettings>();
		var connectionString = databaseSettings.ConnectionString;

		void optionsAction(DbContextOptionsBuilder options) =>
				options
					.UseNpgsql(connectionString);

		services.AddDbContext<StoredDbContext>(optionsAction).AddDbContext<DomainDbContext>(optionsAction);

		services.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped<IDatabase, StoredDbContext>()
				.AddScoped<IPatientRepository, PatientRepository>()
				.AddScoped<IAdministratorRepository, AdministratorRepository>()
				.AddScoped<IContractRepository, ContractRepository>()
				.AddScoped<IOutboxDatabase<DomainEvent>, UnitOfWork>()
				.AddOutbox<DomainEvent>();

		services.Decorate<IOutboxService<DomainEvent>, OutboxTracingService<DomainEvent>>();

		return services;
	}
}
