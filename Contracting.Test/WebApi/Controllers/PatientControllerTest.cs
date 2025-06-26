using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Application.Patients.CreatePatient;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using Contracting.Domain.Patients;
using Contracting.WebApi.Controllers;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

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
		var patientId = Guid.NewGuid();
		var command = new CreatePatientCommand("Alberto Fernandez", "71231237");
		var patientDto = new PatientDto
		{
			Id = patientId,
			PatientName = "Alberto Fernandez",
			PatientPhone = "71231237"
		};

		_mediator
			.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
			.ReturnsAsync(patientId);

		_mediator
			.Setup(m => m.Send(It.Is<GetPatientByIdQuery>(q => q.PatientId == patientId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<AdministratorDto>.Success(patientDto));

		var controller = new PatientController(_mediator.Object);
		var result = await controller.CreatePatient(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		dynamic response = okResult.Value;
		var responseJson = JObject.FromObject(okResult.Value);

		Assert.Equal("Patient created successfully", responseJson["Message"]!.ToString());
		var patientJson = responseJson["Patient"]!;
		Assert.Equal(patientDto.Id.ToString(), patientJson["Id"]!.ToString());
		Assert.Equal(patientDto.PatientName, patientJson["PatientName"]!.ToString());
		Assert.Equal(patientDto.PatientPhone, patientJson["PatientPhone"]!.ToString());
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
		var patientId = Guid.NewGuid();
		var patientDto = new PatientDto
		{
			Id = patientId,
			PatientName = "Alberto Fernandez",
			PatientPhone = "77887878"
		};

		_mediator
			.Setup(m => m.Send(It.Is<GetPatientByIdQuery>(q => q.PatientId == patientId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<PatientDto>.Success(patientDto));

		var controller = new PatientController(_mediator.Object);
		var result = await controller.GetPatientById(patientId);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var responseJson = JObject.FromObject(okResult.Value);

		Assert.Equal("Patient details retrieved successfully", responseJson["Message"]!.ToString());

		var patientJson = responseJson["Patient"]!;
		Assert.Equal(patientId.ToString(), patientJson["Id"]!.ToString());
		Assert.Equal("Alberto Fernandez", patientJson["PatientName"]!.ToString());
		Assert.Equal("77887878", patientJson["PatientPhone"]!.ToString());
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

	[Fact]
	public async Task GetPatientByIdNullTest()
	{
		var id = Guid.Empty;
		var result = await _controller.GetPatientById(id);

		var status = Assert.IsType<NotFoundObjectResult>(result);
		Assert.Equal(StatusCodes.Status404NotFound, status.StatusCode);
	}

}
