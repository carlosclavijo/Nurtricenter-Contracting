using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Contracting.Infrastructure.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.DomainModel;

public class DomainDbContextTest
{
    private readonly DbContextOptions<DomainDbContext> _options;

    public DomainDbContextTest()
    {
        _options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase("DomainDb").Options;
    }

    [Fact]
    public async Task CreateDomainDbContext()
    {
        using var context = new DomainDbContext(_options);
        var model = context.Model;

        var administratorEntity = model.FindEntityType(typeof(Administrator));
        var patientEntity = model.FindEntityType(typeof(Patient));
        var contractEntity = model.FindEntityType(typeof(Contract));
        var domainEventEntity = model.FindEntityType(typeof(DomainEvent));

        Assert.NotNull(administratorEntity);
        Assert.NotNull(patientEntity);
        Assert.NotNull(contractEntity);
        Assert.Null(domainEventEntity);
    }
}
