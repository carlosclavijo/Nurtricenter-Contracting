using System;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Infrastructure.Handlers.Contracts;
using Contracting.Infrastructure.StoredModel;
using Contracting.Infrastructure.StoredModel.Entities;
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
            DeliveryDays = new List<DeliveryDayStoredModel>
            {
                new DeliveryDayStoredModel
                {
                    DeliveryDayId = deliveryDayId1,
                    ContractId = contractId,
                    Contract = null,
                    Date = startDate.AddDays(1),
                    Street = "Grove Street",
                    Number = 30,
                    Longitude = -75.1234,
                    Latitude = 40.1234
                },
                new DeliveryDayStoredModel
                {
                    DeliveryDayId = deliveryDayId2,
                    ContractId = contractId,
                    Contract = null,
                    Date = startDate.AddDays(2),
                    Street = "Elm Street",
                    Number = 100,
                    Longitude = -45.5025,
                    Latitude = 107.2618
                },
                new DeliveryDayStoredModel
                {
                    DeliveryDayId = deliveryDayId3,
                    ContractId = contractId,
                    Contract = null,
                    Date = startDate.AddDays(3),
                    Street = "Paper Street",
                    Number = 50,
                    Longitude = -59.3794,
                    Latitude = 43.4752
                },
            }
        };

        _dbContext.Patient.Add(patient);
        _dbContext.Contract.AddRange(contract);
        _dbContext.SaveChanges();

        var query = new GetContractByIdQuery(contractId);
        var handler = new GetContractByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        Assert.NotNull(result);
        Assert.Equal(contractId, result.Id);
        Assert.Equal(administrtorId, result.AdministratorId);
        Assert.Equal(patientId, result.PatientId);
        Assert.Equal("FullMonth", result.Type);
        Assert.Equal("Created", result.Status);
        Assert.Equal(startDate, result.StartDate);
        Assert.Equal(completedDate, result.CompleteDate);

        Assert.Equal(deliveryDayId1, result.DeliveryDays.First().Id);
        Assert.Equal(deliveryDayId2, result.DeliveryDays.ElementAt(1).Id);
        Assert.Equal(deliveryDayId3, result.DeliveryDays.ElementAt(2).Id);;

        Assert.Equal(contractId, result.DeliveryDays.First().ContractId);
        Assert.Equal(contractId, result.DeliveryDays.ElementAt(1).ContractId);
        Assert.Equal(contractId, result.DeliveryDays.ElementAt(2).ContractId);

        Assert.Equal(startDate.AddDays(1), result.DeliveryDays.First().DateTime);
        Assert.Equal(startDate.AddDays(2), result.DeliveryDays.ElementAt(1).DateTime);
        Assert.Equal(startDate.AddDays(3), result.DeliveryDays.ElementAt(2).DateTime);

        Assert.Equal("Grove Street", result.DeliveryDays.First().Street);
        Assert.Equal("Elm Street", result.DeliveryDays.ElementAt(1).Street);
        Assert.Equal("Paper Street", result.DeliveryDays.ElementAt(2).Street);

        Assert.Equal(-75.1234, result.DeliveryDays.First().Longitude);
        Assert.Equal(-45.5025, result.DeliveryDays.ElementAt(1).Longitude);
        Assert.Equal(-59.3794, result.DeliveryDays.ElementAt(2).Longitude);

        Assert.Equal(40.1234, result.DeliveryDays.First().Latitude);
        Assert.Equal(107.2618, result.DeliveryDays.ElementAt(1).Latitude);
        Assert.Equal(43.4752, result.DeliveryDays.ElementAt(2).Latitude);

    }

    [Fact]
    public void HandleIsInvalid()
    {
        var id = Guid.Empty;

        var query = new GetContractByIdQuery(id);
        var handler = new GetContractByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);

        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(query, cancellationToken.Token));

        Assert.NotNull(exception);
        Assert.Equal("Value cannot be null. (Parameter 'ContractId')", exception.Result.Message);
    }
}
