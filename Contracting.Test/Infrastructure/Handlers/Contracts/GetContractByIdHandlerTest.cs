using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Infrastructure.Handlers.Administrators;
using Contracting.Infrastructure.Handlers.Contracts;
using Contracting.Infrastructure.Persistence.StoredModel;
using Contracting.Infrastructure.Persistence.StoredModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Handlers.Contracts;

public class GetContractByIdHandlerTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private readonly StoredDbContext _dbContext;

    public GetContractByIdHandlerTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("GetContractByIdDb").Options;
        _dbContext = new StoredDbContext(_options);
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var administrtorId = Guid.NewGuid();
        var patientId = Guid.NewGuid();
        var contractId = Guid.NewGuid();
        var deliveryDayId1 = Guid.NewGuid();
        var deliveryDayId2 = Guid.NewGuid();
        var deliveryDayId3 = Guid.NewGuid();

        var administratorName = "Carlos Clavijo";
        var patientName = "Alberto Fernandez";
        var administratorPhone = "78767876";
        var patientPhone = "67552233";

        var startDate = DateTime.UtcNow;
        var completedDate = DateTime.UtcNow.AddDays(30);
        var totalCost = 1500.00m;
        var administrator = new AdministratorStoredModel
        {
            Id = administrtorId,
            AdministratorName = administratorName,
            AdministratorPhone = administratorPhone
        };

        var patient = new PatientStoredModel
        {
            Id = patientId,
            PatientName = patientName,
            PatientPhone = patientPhone
        };

        var contract = new ContractStoredModel
        {
            Id = contractId,
            AdministratorId = administrtorId,
            Administrator = administrator,
            PatientId = patientId,
            Patient = patient,
            TransactionType = "FullMonth",
            TransactionStatus = "Created",
            CreationDate = DateTime.UtcNow,
            StartDate = startDate,
            CompletedDate = completedDate,
            TotalCost = totalCost,
            DeliveryDays =
			[
				new DeliveryDayStoredModel
                {
                    Id = deliveryDayId1,
                    ContractId = contractId,
                    Date = startDate.AddDays(1),
                    Street = "Grove Street",
                    Number = 30,
                },
                new DeliveryDayStoredModel
                {
					Id = deliveryDayId2,
                    ContractId = contractId,
                    Date = startDate.AddDays(2),
                    Street = "Elm Street",
                    Number = 100
                },
                new DeliveryDayStoredModel
                {
					Id = deliveryDayId3,
                    ContractId = contractId,
                    Date = startDate.AddDays(3),
                    Street = "Paper Street",
                    Number = 50
                },
            ]
        };

        _dbContext.Patient.Add(patient);
        _dbContext.Contract.AddRange(contract);
        _dbContext.SaveChanges();

        var query = new GetContractByIdQuery(contractId);
        var handler = new GetContractByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        Assert.NotNull(result);
        Assert.Equal(contractId, result.Value.Id);
        Assert.Equal(administrtorId, result.Value.AdministratorId);
		Assert.Equal(patientId, result.Value.PatientId);
        Assert.Equal("FullMonth", result.Value.Type);
        Assert.Equal("Created", result.Value.Status);
        Assert.Equal(startDate, result.Value.StartDate);
        Assert.Equal(completedDate, result.Value.CompleteDate);

        Assert.Equal(deliveryDayId1, result.Value.DeliveryDays.First().Id);
        Assert.Equal(deliveryDayId2, result.Value.DeliveryDays.ElementAt(1).Id);
        Assert.Equal(deliveryDayId3, result.Value.DeliveryDays.ElementAt(2).Id);;

        Assert.Equal(contractId, result.Value.DeliveryDays.First().ContractId);
        Assert.Equal(contractId, result.Value.DeliveryDays.ElementAt(1).ContractId);
        Assert.Equal(contractId, result.Value.DeliveryDays.ElementAt(2).ContractId);

        Assert.Equal(startDate.AddDays(1), result.Value.DeliveryDays.First().DateTime);
        Assert.Equal(startDate.AddDays(2), result.Value.DeliveryDays.ElementAt(1).DateTime);
        Assert.Equal(startDate.AddDays(3), result.Value.DeliveryDays.ElementAt(2).DateTime);

        Assert.Equal("Grove Street", result.Value.DeliveryDays.First().Street);
        Assert.Equal("Elm Street", result.Value.DeliveryDays.ElementAt(1).Street);
        Assert.Equal("Paper Street", result.Value.DeliveryDays.ElementAt(2).Street);
    }

    [Fact]
    public async Task HandleIsInvalid()
    {
		var options = new DbContextOptionsBuilder<StoredDbContext>()
		.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
		.Options;

		await using var dbContext = new StoredDbContext(options);

		var handler = new GetContractByIdHandler(dbContext);
		var query = new GetContractByIdQuery(Guid.NewGuid());
		var result = await handler.Handle(query, CancellationToken.None);

		Assert.True(result == null || result.IsFailure || result.Value == null);
	}
}
