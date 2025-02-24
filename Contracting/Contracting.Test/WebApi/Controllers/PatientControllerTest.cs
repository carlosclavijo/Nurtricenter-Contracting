using System;
using Contracting.Application.Patients.CreatePatient;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using Contracting.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Contracting.Test.WebApi.Controllers;

public class PatientControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly PatientController _controller;

    public PatientControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new PatientController(_mediator.Object);
    }

    [Fact]
    public async Task CreatePatientValidTest()
    {
        var name = "Carlos Clavijo";
        var phone = "77601415";
        var command = new CreatePatientCommand(name, phone);
        var result = await _controller.CreatePatient(command);
        var createdResult = Assert.IsType<ObjectResult>(result);

        Assert.NotNull(createdResult);
    }

    [Fact]
    public async Task CreateAdministratorInvalidTest()
    {
        var name = "Carlos Clavijo";
        var phone = "77601415";
        var command = new CreatePatientCommand(name, phone);

        _mediator.Setup(m => m.Send(It.IsAny<CreatePatientCommand>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.CreatePatient(command);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal("Something went wrong", statusCodeResult.Value);
    }

    [Fact]
    public async Task GetPatientsValidTest ()
    {
        var result = await _controller.GetPatients();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPatientsInvalidTest()
    {
        _mediator.Setup(m => m.Send(It.IsAny<GetPatientsQuery>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.GetPatients();

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, status.StatusCode);
        Assert.Equal("Something went wrong", status.Value);
    }

    [Fact]
    public async Task GetPatientByIdValidTest()
    {
        var id = Guid.NewGuid();

        var result = await _controller.GetPatientById(id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPatientByIdInValidTest()
    {
        var id = Guid.NewGuid();

        _mediator.Setup(m => m.Send(It.IsAny<GetPatientByIdQuery>(), default)).ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.GetPatientById(id);

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, status.StatusCode);
        Assert.Equal("Something went wrong", status.Value);
    }
}
