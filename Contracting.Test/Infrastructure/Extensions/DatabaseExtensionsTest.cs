using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Contracting.Infrastructure.Extensions;
using Contracting.Infrastructure.Persistence;
using Contracting.Infrastructure.Persistence.DomainModel;
using Contracting.Infrastructure.Persistence.StoredModel;
using Joseco.Outbox.Contracts.Service;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nur.Store2025.Observability.Tracing;

namespace Contracting.Test.Infrastructure.Extensions;

public class DatabaseExtensionsTest
{
	[Fact]
	public void AddDatabaseIsValid()
	{
		var services = new ServiceCollection();

		var settings = new DatabaseSettings
		{
			ConnectionString = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=pass"
		};

		var tracingProviderMock = new Mock<ITracingProvider>();
		tracingProviderMock.Setup(tp => tp.GetCorrelationId()).Returns("fake-correlation-id");
		tracingProviderMock.Setup(tp => tp.GetTraceId()).Returns("fake-trace-id");
		tracingProviderMock.Setup(tp => tp.GetSpanId()).Returns("fake-span-id");

		services.AddSingleton(tracingProviderMock.Object);

		services.AddSingleton(settings);

		services.AddEntityFrameworkNpgsql();

		services.AddDatabase();
		var provider = services.BuildServiceProvider();

		Assert.NotNull(provider.GetService<StoredDbContext>());
		Assert.NotNull(provider.GetService<DomainDbContext>());
		Assert.NotNull(provider.GetService<IUnitOfWork>());
		Assert.NotNull(provider.GetService<IContractRepository>());
		Assert.NotNull(provider.GetService<IPatientRepository>());
		Assert.NotNull(provider.GetService<IAdministratorRepository>());
		Assert.NotNull(provider.GetService<IOutboxDatabase<DomainEvent>>());
		Assert.NotNull(provider.GetService<IOutboxService<DomainEvent>>());
	}
}
