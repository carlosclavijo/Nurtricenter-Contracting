using System;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.Persistence.StoredModel.Entities;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence.StoredModel;

public class StoredDbContext(DbContextOptions<StoredDbContext> options) : DbContext(options), IDatabase
{
	public DbSet<AdministratorStoredModel> Administrator { get; set; }
	public DbSet<PatientStoredModel> Patient { get; set; }
	public DbSet<ContractStoredModel> Contract { get; set; }
	public DbSet<DeliveryDayStoredModel> DeliveryDay { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.AddOutboxModel<DomainEvent>();
	}

	public void Migrate()
	{
		Database.Migrate();
	}
}