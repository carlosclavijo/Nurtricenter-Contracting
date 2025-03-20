using System;
using Contracting.Application;
using Contracting.Domain.Administrators;
using Contracting.Domain.Contracts;
using Contracting.Domain.Patients;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Contracting.Test.Application;

public class ExtensionsTest
{
    private readonly ServiceCollection _services;

    public ExtensionsTest()
    {
        _services = new ServiceCollection();
    }

    [Fact]
    public void AddApplicationTest()
    {
        _services.AddApplication();

        var serviceProvider = _services.BuildServiceProvider();
        var mediator = serviceProvider.GetService<IMediator>();
        var administratorFactory = serviceProvider.GetService<IAdministratorFactory>();
        var patientFactory = serviceProvider.GetService<IPatienteFactory>();
        var contractFactory = serviceProvider.GetService<IContractFactory>();
        
        Assert.NotNull(mediator);
        Assert.IsAssignableFrom<IMediator>(mediator);

        Assert.NotNull(administratorFactory);
        Assert.IsType<AdministratorFactory>(administratorFactory);
        
        Assert.NotNull(patientFactory);
        Assert.IsType<PatientFactory>(patientFactory);

        Assert.NotNull(contractFactory);
        Assert.IsType<ContractFactory>(contractFactory);

        var administratorFactory1 = serviceProvider.GetService<IAdministratorFactory>();
        var administratorFactory2 = serviceProvider.GetService<IAdministratorFactory>();
        Assert.Same(administratorFactory1, administratorFactory2);

        var patientFactory1 = serviceProvider.GetService<IPatienteFactory>();
        var patientFactory2 = serviceProvider.GetService<IPatienteFactory>();
        Assert.Same(patientFactory1, patientFactory2);

        var contractFacatory1 = serviceProvider.GetService<IContractFactory>();
        var contractFacatory2 = serviceProvider.GetService<IContractFactory>();
        Assert.Same(contractFacatory1, contractFacatory2);

    }
}
