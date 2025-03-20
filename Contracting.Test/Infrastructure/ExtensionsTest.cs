using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using Contracting.Infrastructure;
using Contracting.Infrastructure.DomainModel;
using Contracting.Infrastructure.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore.InMemory.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace Contracting.Test.Infrastructure;

public class ExtensionsTest
{
    private readonly Mock<IConfiguration> _configuration;
    private readonly ServiceCollection _services;
    private readonly Dictionary<string, string> _memory;

    public ExtensionsTest()
    {
        _configuration = new Mock<IConfiguration>();
        _services = new ServiceCollection();
        _memory = new Dictionary<string, string>
        {
            { "ConnectionStrings:DefaultConnection", "Host=localhost;Database=testdb;Username=testuser;Password=testpassword" }
        };
    }

    [Fact]
    public void AddInfrastructureTest()
    {
        _configuration.Setup(c => c.GetSection("ConnectionStrings"))
                      .Returns(new ConfigurationSection(new ConfigurationBuilder().AddInMemoryCollection(_memory).Build(), "ConnectionStrings"));


        _services.AddInfrastructure(_configuration.Object);

        var serviceProvider = _services.BuildServiceProvider();

        var dbContext = serviceProvider.GetService<DomainDbContext>();
        var storedDbContext = serviceProvider.GetService<StoredDbContext>();
        var mediator = serviceProvider.GetService<IMediator>();
        var administratorRepository = serviceProvider.GetService<IAdministratorRepository>();
        var patientRepository = serviceProvider.GetService<IPatientRepository>();
        var contractRepository = serviceProvider.GetService<IContractRepository>();
        var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        
        Assert.NotNull(dbContext);
        Assert.IsType<DomainDbContext>(dbContext);

        Assert.NotNull(storedDbContext);
        Assert.IsType<StoredDbContext>(storedDbContext);

        Assert.NotNull(mediator);
        Assert.IsAssignableFrom<IMediator>(mediator);

        Assert.NotNull(administratorRepository);
        Assert.IsAssignableFrom<IAdministratorRepository>(administratorRepository);

        Assert.NotNull(patientRepository);
        Assert.IsAssignableFrom<IPatientRepository>(patientRepository);

        Assert.NotNull(contractRepository);
        Assert.IsAssignableFrom<IContractRepository>(contractRepository);

        Assert.NotNull(unitOfWork);
        Assert.IsAssignableFrom<IUnitOfWork>(unitOfWork);

        var administratorRepository1 = serviceProvider.GetService<IAdministratorRepository>();
        var administratorRepository2 = serviceProvider.GetService<IAdministratorRepository>();
        Assert.Same(administratorRepository1, administratorRepository2);

        var patientRepository1 = serviceProvider.GetService<IAdministratorRepository>();
        var patientRepository2 = serviceProvider.GetService<IAdministratorRepository>();
        Assert.Same(patientRepository1, patientRepository2);

        var contractRepository1 = serviceProvider.GetService<IAdministratorRepository>();
        var contractRepository2 = serviceProvider.GetService<IAdministratorRepository>();
        Assert.Same(contractRepository1, contractRepository2);

        var unitOfWork1 = serviceProvider.GetService<IUnitOfWork>();
        var unitOfWork2 = serviceProvider.GetService<IUnitOfWork>();
        Assert.Same(unitOfWork1, unitOfWork2);
    }
}
