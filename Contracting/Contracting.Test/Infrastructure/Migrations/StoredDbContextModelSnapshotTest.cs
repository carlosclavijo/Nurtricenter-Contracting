using System;
using Contracting.Infrastructure.Migrations;
using Contracting.Infrastructure.StoredModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Migrations;

public class StoredDbContextModelSnapshotTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private StoredDbContext _context;

    public StoredDbContextModelSnapshotTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("StoredDbContextDb").Options;
        _context = new StoredDbContext(_options);
    }

    [Fact]
    public void StoredDbContextModelSnapshotValidTest()
    {
        var modelSnapshot = new StoredDbContextModelSnapshot();

        var entityTypes = modelSnapshot.Model.GetEntityTypes().ToList();

        var administratorsTable = entityTypes.FirstOrDefault(x => x.GetTableName() == "administrators");
        var contractsTable = entityTypes.FirstOrDefault(x => x.GetTableName() == "contracts");
        var patientsTable = entityTypes.FirstOrDefault(x => x.GetTableName() == "patients");
        var deliveryDaysTable = entityTypes.FirstOrDefault(x => x.GetTableName() == "deliverydays");

        Assert.NotNull(administratorsTable);
        Assert.NotNull(contractsTable);
        Assert.NotNull(patientsTable);
        Assert.NotNull(deliveryDaysTable);
    }
}
