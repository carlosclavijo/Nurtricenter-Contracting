using System;
using Contracting.Infrastructure.Handlers.Administrators;
using Contracting.Infrastructure.StoredModel.Entities;
using Contracting.Infrastructure.StoredModel;
using Microsoft.EntityFrameworkCore;
using Contracting.Application.Administrators.GetAdministratorById;
using System.Numerics;
using System.Xml.Linq;

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
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.AdministratorName);
        Assert.Equal(phone, result.AdministratorPhone);
    }

    [Fact]
    public void HandleIsInvalid()
    {
        var id = Guid.Empty;

        var query = new GetAdministratorByIdQuery(id);
        var handler = new GetAdministratorByIdHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);

        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(query, cancellationToken.Token));

        Assert.NotNull(exception);
        Assert.Equal("Value cannot be null. (Parameter 'AdministratorId')", exception.Result.Message);
    }
}
