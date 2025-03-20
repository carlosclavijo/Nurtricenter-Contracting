using System;
using Contracting.Infrastructure.Handlers.Patients;
using Contracting.Infrastructure.StoredModel.Entities;
using Contracting.Infrastructure.StoredModel;
using Microsoft.EntityFrameworkCore;
using Contracting.Application.Patients.GetPatients;

namespace Contracting.Test.Infrastructure.Handlers.Patients;

public class GetPatientsHandlerTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private readonly StoredDbContext _dbContext;

    public GetPatientsHandlerTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("GetPatientByIdDb").Options;
        _dbContext = new StoredDbContext(_options);
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var name1 = "Carlos Clavijo";
        var name2 = "Alberto Fernandez";
        var phone1 = "78767876";
        var phone2 = "77668855";

        _dbContext.Patient.AddRange(
            new PatientStoredModel
            {
                Id = id1,
                PatientName = name1,
                PatientPhone = phone1
            },
            new PatientStoredModel
            {
                Id = id2,
                PatientName = name2,
                PatientPhone = phone2
            }
        );

        _dbContext.SaveChanges();

        var query = new GetPatientsQuery("");
        var handler = new GetPatientsHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(id1, result.First().Id);
        Assert.Equal(id2, result.ElementAt(1).Id);
        Assert.Equal(name1, result.First().PatientName);
        Assert.Equal(name2, result.ElementAt(1).PatientName);
        Assert.Equal(phone1, result.First().PatientPhone);
        Assert.Equal(phone2, result.ElementAt(1).PatientPhone);
    }
}
