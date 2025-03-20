using System;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Infrastructure.Handlers.Administrators;
using Contracting.Infrastructure.StoredModel.Entities;
using Contracting.Infrastructure.StoredModel;
using Microsoft.EntityFrameworkCore;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Infrastructure.Handlers.Patients;

namespace Contracting.Test.Infrastructure.Handlers.Patients;

public class GetPatientByIdHandlerTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private readonly StoredDbContext _dbContext;

    public GetPatientByIdHandlerTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("GetPatientsDb").Options;
        _dbContext = new StoredDbContext(_options);
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var id = Guid.NewGuid();
        var name = "Carlos Clavijo";
        var phone = "78767876";

        _dbContext.Patient.AddRange(
            new PatientStoredModel
            {
                Id = id,
                PatientName = name,
                PatientPhone = phone
            }
        );

        _dbContext.SaveChanges();

        var query = new GetPatientByIdQuery(id);
        var handler = new GetPatientByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.PatientName);
        Assert.Equal(phone, result.PatientPhone);
    }

    [Fact]
    public void HandleIsInvalid()
    {
        var id = Guid.Empty;

        var query = new GetPatientByIdQuery(id);
        var handler = new GetPatientByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);

        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(query, cancellationToken.Token));

        Assert.NotNull(exception);
        Assert.Equal("Value cannot be null. (Parameter 'PatientId')", exception.Result.Message);
    }
}
