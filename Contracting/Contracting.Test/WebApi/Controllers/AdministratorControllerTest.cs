using System;
using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Contracting.Test.WebApi.Controllers;

public class AdministratorControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly AdministratorController _controller;

    public AdministratorControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new AdministratorController(_mediator.Object);
    }

    [Fact]
    public async Task CreateAdministratorValidTest()
    {
        var name = "Carlos Clavijo";
        var phone = "77601415";
        var command = new CreateAdministratorCommand(name, phone);
        var result = await _controller.CreateAdministrator(command);
        var createdResult = Assert.IsType<CreatedResult>(result);

        Assert.NotNull(createdResult);
    }

    [Fact]
    public async Task CreateAdministratorInvalidTest()
    {
        var name = "Carlos Clavijo";
        var phone = "77601415";
        var command = new CreateAdministratorCommand(name, phone);

        _mediator.Setup(m => m.Send(It.IsAny<CreateAdministratorCommand>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.CreateAdministrator(command);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Something went wrong", statusCodeResult.Value);
    }

    [Fact]
    public async Task GetAdministratorsValidTest()
    {
        var result = await _controller.GetAdministrators();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAdministratorsInvalidTest()
    {
        _mediator.Setup(m => m.Send(It.IsAny<GetAdministratorsQuery>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.GetAdministrators();

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, status.StatusCode);
        Assert.Equal("Something went wrong", status.Value);
    }

    [Fact]
    public async Task GetAdministratorByIdValidTest()
    {
        var id = Guid.NewGuid();

        var result = await _controller.GetAdministratorById(id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAdministratorByIdInValidTest()
    {
        var id = Guid.NewGuid();

        _mediator.Setup(m => m.Send(It.IsAny<GetAdministratorByIdQuery>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.GetAdministratorById(id);

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, status.StatusCode);
        Assert.Equal("Something went wrong", status.Value);
    }
}
