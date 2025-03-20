using System;
using Contracting.Infrastructure.StoredModel;
using Microsoft.EntityFrameworkCore;
using Contracting.Infrastructure.StoredModel.Entities;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Infrastructure.Handlers.Contracts;
using System.Linq;

namespace Contracting.Test.Infrastructure.Handlers.Contracts;

public class GetContractsHandlerTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private readonly StoredDbContext _dbContext;

    public GetContractsHandlerTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("GetContractsDb").Options;
        _dbContext = new StoredDbContext(_options);
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var administrtorId = Guid.NewGuid();
        var patientId = Guid.NewGuid();
        var contractId1 = Guid.NewGuid();
        var contractId2 = Guid.NewGuid();
        var deliveryDayId1 = Guid.NewGuid();
        var deliveryDayId2 = Guid.NewGuid();
        var deliveryDayId3 = Guid.NewGuid();
        var deliveryDayId4 = Guid.NewGuid();
        var deliveryDayId5 = Guid.NewGuid();

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

        var contract1 = new ContractStoredModel
        {
            Id = contractId1,
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
                    ContractId = contractId1,
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
                    ContractId = contractId1,
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
                    ContractId = contractId1,
                    Contract = null,
                    Date = startDate.AddDays(3),
                    Street = "Paper Street",
                    Number = 50,
                    Longitude = -59.3794,
                    Latitude = 43.4752
                },
            }
        };

        var contract2 = new ContractStoredModel
        {
            Id = contractId2,
            AdministratorId = administrtorId,
            Administrator = administrator,
            PatientId = patientId,
            Patient = patient,
            TransactionType = "HalfMonth",
            TransactionStatus = "Created",
            CreationDate = DateTime.UtcNow,
            StartDate = startDate,
            CompletedDate = completedDate,
            TotalCost = totalCost,
            DeliveryDays = new List<DeliveryDayStoredModel>
            {
                new DeliveryDayStoredModel
                {
                    DeliveryDayId = deliveryDayId4,
                    ContractId = contractId2,
                    Contract = null,
                    Date = startDate.AddDays(10),
                    Street = "Spooner Street",
                    Number = 200,
                    Longitude = -24.1953,
                    Latitude = 119.2496
                },
                new DeliveryDayStoredModel
                {
                    DeliveryDayId = deliveryDayId5,
                    ContractId = contractId2,
                    Contract = null,
                    Date = startDate.AddDays(11),
                    Street = "Evergreen Terrace",
                    Number = 150,
                    Longitude = -12.8486,
                    Latitude = 94.1127
                }
            }
        };

        _dbContext.Administrator.Add(administrator);
        _dbContext.Patient.Add(patient);
        _dbContext.Contract.AddRange(contract1, contract2);
        _dbContext.SaveChanges();

        var query = new GetContractsQuery("");
        var handler = new GetContractsHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        // Validación de resultados
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(contractId1, result.First().Id);
        Assert.Equal(contractId2, result.ElementAt(1).Id);
        Assert.Equal(administrtorId, result.First().AdministratorId);
        Assert.Equal(administrtorId, result.ElementAt(1).AdministratorId);
        Assert.Equal(patientId, result.First().PatientId);
        Assert.Equal(patientId, result.ElementAt(1).PatientId);
        Assert.Equal("FullMonth", result.First().Type);
        Assert.Equal("HalfMonth", result.ElementAt(1).Type);
        Assert.Equal("Created", result.First().Status);
        Assert.Equal("Created", result.ElementAt(1).Status);
        Assert.Equal(startDate, result.First().StartDate);
        Assert.Equal(startDate, result.ElementAt(1).StartDate);
        Assert.Equal(completedDate, result.First().CompleteDate);
        Assert.Equal(completedDate, result.ElementAt(1).CompleteDate);

        Assert.Equal(deliveryDayId1, result.First().DeliveryDays.First().Id);
        Assert.Equal(deliveryDayId2, result.First().DeliveryDays.ElementAt(1).Id);
        Assert.Equal(deliveryDayId3, result.First().DeliveryDays.ElementAt(2).Id);
        Assert.Equal(deliveryDayId4, result.ElementAt(1).DeliveryDays.First().Id);
        Assert.Equal(deliveryDayId5, result.ElementAt(1).DeliveryDays.ElementAt(1).Id);

        Assert.Equal(contractId1, result.First().DeliveryDays.First().ContractId);
        Assert.Equal(contractId1, result.First().DeliveryDays.ElementAt(1).ContractId);
        Assert.Equal(contractId1, result.First().DeliveryDays.ElementAt(2).ContractId);
        Assert.Equal(contractId2, result.ElementAt(1).DeliveryDays.First().ContractId);
        Assert.Equal(contractId2, result.ElementAt(1).DeliveryDays.ElementAt(1).ContractId);

        Assert.Equal(startDate.AddDays(1), result.First().DeliveryDays.First().DateTime);
        Assert.Equal(startDate.AddDays(2), result.First().DeliveryDays.ElementAt(1).DateTime);
        Assert.Equal(startDate.AddDays(3), result.First().DeliveryDays.ElementAt(2).DateTime);
        Assert.Equal(startDate.AddDays(10), result.ElementAt(1).DeliveryDays.First().DateTime);
        Assert.Equal(startDate.AddDays(11), result.ElementAt(1).DeliveryDays.ElementAt(1).DateTime);

        Assert.Equal("Grove Street", result.First().DeliveryDays.First().Street);
        Assert.Equal("Elm Street", result.First().DeliveryDays.ElementAt(1).Street);
        Assert.Equal("Paper Street", result.First().DeliveryDays.ElementAt(2).Street);
        Assert.Equal("Spooner Street", result.ElementAt(1).DeliveryDays.First().Street);
        Assert.Equal("Evergreen Terrace", result.ElementAt(1).DeliveryDays.ElementAt(1).Street);

        Assert.Equal(-75.1234, result.First().DeliveryDays.First().Longitude);
        Assert.Equal(-45.5025, result.First().DeliveryDays.ElementAt(1).Longitude);
        Assert.Equal(-59.3794, result.First().DeliveryDays.ElementAt(2).Longitude);
        Assert.Equal(-24.1953, result.ElementAt(1).DeliveryDays.First().Longitude);
        Assert.Equal(-12.8486, result.ElementAt(1).DeliveryDays.ElementAt(1).Longitude);

        Assert.Equal(40.1234, result.First().DeliveryDays.First().Latitude);
        Assert.Equal(107.2618, result.First().DeliveryDays.ElementAt(1).Latitude);
        Assert.Equal(43.4752, result.First().DeliveryDays.ElementAt(2).Latitude);
        Assert.Equal(119.2496, result.ElementAt(1).DeliveryDays.First().Latitude);
        Assert.Equal(94.1127, result.ElementAt(1).DeliveryDays.ElementAt(1).Latitude);

    }
}
