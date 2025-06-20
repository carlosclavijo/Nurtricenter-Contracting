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
        var patient = new Patient("Carlos Clavijo", "77601415");

		_mediator
			.Setup(m => m.Send(It.IsAny<CreatePatientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(patient.Id);

		_mediator
		   .Setup(m => m.Send(It.Is<GetPatientByIdQuery>(q => q.PatientId == patient.Id), It.IsAny<CancellationToken>()))
           .ReturnsAsync(Result<PatientDto>.Success(new PatientDto
           {
               Id = patient.Id,
               PatientName = patient.Name,
               PatientPhone = patient.Phone
           }));

        var controller = new PatientController(_mediator.Object);
        var command = new CreatePatientCommand("Carlos Clavijo", "77601415");
        var result = await controller.CreatePatient(command);
        var okResult = Assert.IsType<OkObjectResult>(result);
		var value = okResult.Value;
		var valueType = value.GetType();

		var messageProp = valueType.GetProperty("Message");
		var patientProp = valueType.GetProperty("Patient");

		var message = (string)messageProp.GetValue(value);
		var patientResult = patientProp.GetValue(value);

		var patientResultType = patientResult.GetType();
		var valueProp = patientResultType.GetProperty("Value");
		var patientDto = valueProp.GetValue(patientResult);

		var patientDtoType = patientDto.GetType();
		var id = (Guid)patientDtoType.GetProperty("Id").GetValue(patientDto);
		var name = (string)patientDtoType.GetProperty("PatientName").GetValue(patientDto);
		var phone = (string)patientDtoType.GetProperty("PatientPhone").GetValue(patientDto);

		Assert.Equal("Patient created sucessfully", message);
		Assert.Equal(patient.Id, id);
		Assert.Equal("Carlos Clavijo", name);
		Assert.Equal("77601415", phone);
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
			PatientName = "Carlos Clavijo",
			PatientPhone = "77601415"
		};

		_mediator
			.Setup(m => m.Send(It.Is<GetPatientByIdQuery>(q => q.PatientId == patientId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<PatientDto>.Success(patientDto));

		var controller = new PatientController(_mediator.Object);
		var result = await controller.GetPatientById(patientId);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var value = okResult.Value;
		var valueType = value.GetType();

		var messageProp = valueType.GetProperty("Message");
		var patientProp = valueType.GetProperty("Patient");

		var message = (string)messageProp.GetValue(value);
		var patientResult = patientProp.GetValue(value);

		var patientResultType = patientResult.GetType();
		var valueProp = patientResultType.GetProperty("Value");
		var patientDtoValue = valueProp.GetValue(patientResult);

		var patientDtoType = patientDtoValue.GetType();
		var id = (Guid)patientDtoType.GetProperty("Id").GetValue(patientDtoValue);
		var name = (string)patientDtoType.GetProperty("PatientName").GetValue(patientDtoValue);
		var phone = (string)patientDtoType.GetProperty("PatientPhone").GetValue(patientDtoValue);

		Assert.NotNull(value);
		Assert.Equal("Patient details retrieved successfully", message);
		Assert.Equal(patientId, id);
		Assert.Equal("Carlos Clavijo", name);
		Assert.Equal("77601415", phone);
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
