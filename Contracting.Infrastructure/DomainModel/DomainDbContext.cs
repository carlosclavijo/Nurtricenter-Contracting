using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.DomainModel;

public class DomainDbContext : DbContext
{

    public DbSet<Administrator> Administrator { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Contract> Contract { get; set; }

    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomainDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<DomainEvent>();
    }
}
