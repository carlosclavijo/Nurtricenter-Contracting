using System;
using Contracting.Infrastructure.StoredModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.StoredModel;

public class StoredDbContext : DbContext
{
    public DbSet<AdministratorStoredModel> Administrator { get; set; }
    public DbSet<PatientStoredModel> Patient { get; set; }
    public DbSet<ContractStoredModel> Contract { get; set; }
    public DbSet<DeliveryDayStoredModel> DeliveryDay { get; set; }

    public StoredDbContext(DbContextOptions<StoredDbContext> options) : base(options)
    {

    }
}