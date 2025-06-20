using Contracting.Infrastructure.Handlers.Administrators;
using Microsoft.EntityFrameworkCore;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Infrastructure.Persistence.StoredModel;
using Contracting.Infrastructure.Persistence.StoredModel.Entities;
using Moq;
using System;

namespace Contracting.Test.Infrastructure.Handlers.Administrator;

public class GetAdministratorByIdHandlerTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private readonly StoredDbContext _dbContext;

    public GetAdministratorByIdHandlerTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("GetAdminsDb").Options;
        _dbContext = new StoredDbContext(_options);
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var id = Guid.NewGuid();
        var name = "Carlos Clavijo";
        var phone = "78767876";

        _dbContext.Administrator.AddRange(
            new AdministratorStoredModel
            {
                Id = id,
                AdministratorName = name,
                AdministratorPhone = phone
            }
        );

        _dbContext.SaveChanges();

        var query = new GetAdministratorByIdQuery(id);
        var handler = new GetAdministratorByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        Assert.NotNull(result);
        Assert.Equal(id, result.Value.Id);
        Assert.Equal(name, result.Value.AdministratorName);
        Assert.Equal(phone, result.Value.AdministratorPhone);
    }

	[Fact]
	public async Task HandleIsInvalid()
	{
		var options = new DbContextOptionsBuilder<StoredDbContext>()
		.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
		.Options;

		await using var dbContext = new StoredDbContext(options);

		var handler = new GetAdministratorByIdHandler(dbContext);
		var query = new GetAdministratorByIdQuery(Guid.NewGuid());
		var result = await handler.Handle(query, CancellationToken.None);

		Assert.True(result == null || result.IsFailure || result.Value == null);
	}
}
