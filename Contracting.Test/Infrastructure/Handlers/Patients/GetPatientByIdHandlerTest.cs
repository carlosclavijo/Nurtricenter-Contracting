using Microsoft.EntityFrameworkCore;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Infrastructure.Handlers.Patients;
using Contracting.Infrastructure.Persistence.StoredModel;
using Contracting.Infrastructure.Persistence.StoredModel.Entities;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Infrastructure.Handlers.Administrators;

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
        Assert.Equal(id, result.Value.Id);
        Assert.Equal(name, result.Value.PatientName);
        Assert.Equal(phone, result.Value.PatientPhone);
    }

	[Fact]
	public async Task HandleIsInvalid()
	{
		var options = new DbContextOptionsBuilder<StoredDbContext>()
		.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
		.Options;

		await using var dbContext = new StoredDbContext(options);

		var handler = new GetPatientByIdHandler(dbContext);
		var query = new GetPatientByIdQuery(Guid.NewGuid());
		var result = await handler.Handle(query, CancellationToken.None);

		Assert.True(result == null || result.IsFailure || result.Value == null);
	}
}
