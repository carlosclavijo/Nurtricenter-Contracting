using System;
using System.Collections.ObjectModel;
using Contracting.Application.Contracts.CompleteContract;
using Contracting.Application.Contracts.CreateContract;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Application.Contracts.InProgressContract;
using Contracting.Application.Contracts.UpdateAddressById;
using Contracting.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contracting.Test.WebApi.Controllers;

public class ContractControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly ContractController _controller;

    public ContractControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new ContractController(_mediator.Object);
    }

    private CreateContractCommand contract()
    {
        var startDelivery1 = DateTime.UtcNow.AddDays(2);
        var street1 = "Grove Street";
        var number1 = 30;
        var longitude1 = -45.1893;
        var latitude1 = 32.9132;

        var startDelivery2 = DateTime.UtcNow.AddDays(3);
        var street2 = "Spooner Street";
        var number2 = 30;
        var longitude2 = -27.3821;
        var latitude2 = -19.9486;

        var deliveryCommand1 = new CreateDeliveryDaysCommand(startDelivery1, street1, number1, longitude1, latitude1);
        var deliveryCommand2 = new CreateDeliveryDaysCommand(startDelivery2, street2, number2, longitude2, latitude2);

        var administratorId = Guid.NewGuid();
        var patientId = Guid.NewGuid();
        var type = "FullMonth";
        var startDate = DateTime.UtcNow;
        var days = new Collection<CreateDeliveryDaysCommand> { deliveryCommand1, deliveryCommand2 };

        return new CreateContractCommand(administratorId, patientId, type, startDate, days);
    }

    [Fact]
    public async Task CreateContractValidTest()
    {
        var command = contract();
        var result = await _controller.CreateContract(command);
        var createdResult = Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(createdResult);
    }

    [Fact]
    public async Task CreateContractInvalidTest()
    {
        var command = contract();

        _mediator.Setup(m => m.Send(It.IsAny<CreateContractCommand>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.CreateContract(command);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Something went wrong", statusCodeResult.Value);
    }

    [Fact]
    public async Task GetContractsValidTest()
    {
        var result = await _controller.GetContracts();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetContractsInvalidTest()
    {
        _mediator.Setup(m => m.Send(It.IsAny<GetContractsQuery>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.GetContracts();

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, status.StatusCode);
        Assert.Equal("Something went wrong", status.Value);
    }

    [Fact]
    public async Task GetContractByIdValidTest()
    {
        var id = Guid.NewGuid();

        var result = await _controller.GetContractById(id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetContractByIdInvalidTest()
    {
        var id = Guid.NewGuid();

        _mediator.Setup(m => m.Send(It.IsAny<GetContractByIdQuery>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.GetContractById(id);

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, status.StatusCode);
        Assert.Equal("Something went wrong", status.Value);
    }

    [Fact]
    public async Task InProgressContractValidTest()
    {
        var id = Guid.NewGuid();
        var command = new InProgressContractCommand(id);
        var result = await _controller.InProgressContract(command);
        var createdResult = Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(createdResult);
    }

    [Fact]
    public async Task InProgressContractInvalidTest()
    {
        var id = Guid.NewGuid();
        var command = new InProgressContractCommand(id);

        _mediator.Setup(m => m.Send(It.IsAny<InProgressContractCommand>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.InProgressContract(command);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Something went wrong", statusCodeResult.Value);
    }

    [Fact]
    public async Task CompleteContractValidTest()
    {
        var id = Guid.NewGuid();
        var command = new CompleteContractCommand(id);
        var result = await _controller.CompleteContract(command);
        var createdResult = Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(createdResult);
    }

    [Fact]
    public async Task CompleteContractInvalidTest()
    {
        var id = Guid.NewGuid();
        var command = new CompleteContractCommand(id);

        _mediator.Setup(m => m.Send(It.IsAny<CompleteContractCommand>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.CompleteContract(command);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Something went wrong", statusCodeResult.Value);
    }

    [Fact]
    public async Task UpdateAddressDatesValidTest()
    {
        var id = Guid.NewGuid();
        var fromDay = DateTime.UtcNow.AddDays(2);
        var toDay = DateTime.UtcNow.AddDays(5);
        var street = "Sesame Street";
        var number = 200;
        var longitude = -99.2831;
        var latitude = -2.3232;

        var command = new UpdateAddressCommand(id, fromDay, toDay, street, number, longitude, latitude);
        var result = await _controller.UpdateAddressDates(command);
        var createdResult = Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(createdResult);
    }

    [Fact]
    public async Task UpdateAddressDatesInvalidTest()
    {
        var id = Guid.NewGuid();
        var fromDay = DateTime.UtcNow.AddDays(2);
        var toDay = DateTime.UtcNow.AddDays(5);
        var street = "Sesame Street";
        var number = 200;
        var longitude = -99.2831;
        var latitude = -2.3232;

        var command = new UpdateAddressCommand(id, fromDay, toDay, street, number, longitude, latitude);

        _mediator.Setup(m => m.Send(It.IsAny<UpdateAddressCommand>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.UpdateAddressDates(command);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Something went wrong", statusCodeResult.Value);
    }
}
