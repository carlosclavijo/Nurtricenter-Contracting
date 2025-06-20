using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Infrastructure.Handlers.Administrators;
using Contracting.Infrastructure.Persistence.StoredModel;
using Contracting.Infrastructure.Persistence.StoredModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Handlers.Administrator;

public class GetAdministratorsHandlerTest
{
    private readonly DbContextOptions<StoredDbContext> _options;
    private readonly StoredDbContext _dbContext;

    public GetAdministratorsHandlerTest()
    {
        _options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase("GetAdminByIdDb").Options;
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

        _dbContext.Administrator.AddRange(
            new AdministratorStoredModel
            {
                Id = id1,
                AdministratorName = name1,
                AdministratorPhone = phone1
            },
            new AdministratorStoredModel
            {
                Id = id2,
                AdministratorName = name2,
                AdministratorPhone = phone2
            }
        );

        _dbContext.SaveChanges();

        var query = new GetAdministratorsQuery("");
        var handler = new GetAdministratorsHandler(_dbContext);
        var cancellationToken = new CancellationTokenSource(1000);
        var result = await handler.Handle(query, cancellationToken.Token);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(id1, result.First().Id);
        Assert.Equal(id2, result.ElementAt(1).Id);
        Assert.Equal(name1, result.First().AdministratorName);
        Assert.Equal(name2, result.ElementAt(1).AdministratorName);
        Assert.Equal(phone1, result.First().AdministratorPhone);
        Assert.Equal(phone2, result.ElementAt(1).AdministratorPhone);
    }
}