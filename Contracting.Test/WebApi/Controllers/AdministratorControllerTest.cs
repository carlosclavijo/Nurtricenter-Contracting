using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using Contracting.Domain.Administrators;
using Contracting.WebApi.Controllers;
using Joseco.DDD.Core.Results;
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
		var administrator = new Administrator("Carlos Clavijo", "77601415");

		_mediator
			.Setup(m => m.Send(It.IsAny<CreateAdministratorCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(administrator.Id);

		_mediator
		   .Setup(m => m.Send(It.Is<GetAdministratorByIdQuery>(q => q.AdministratorId == administrator.Id), It.IsAny<CancellationToken>()))
		   .ReturnsAsync(Result<Administrator>.Success(new AdministratorDto
		   {
			   Id = administrator.Id,
			   AdministratorName = administrator.Name,
			   AdministratorPhone = administrator.Phone
		   }));

		var controller = new AdministratorController(_mediator.Object);
		var command = new CreateAdministratorCommand("Carlos Clavijo", "77601415");
		var result = await controller.CreateAdministrator(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var value = okResult.Value;
		var valueType = value.GetType();

		var messageProp = valueType.GetProperty("Message");
		var administratorProp = valueType.GetProperty("Administrator");

		var message = (string)messageProp.GetValue(value);
		var administratorResult = administratorProp.GetValue(value);

		var administratorResultType = administratorResult.GetType();
		var valueProp = administratorResultType.GetProperty("Value");
		var administratorDto = valueProp.GetValue(administratorResult);

		var administratorDtoType = administratorDto.GetType();
		var id = (Guid)administratorDtoType.GetProperty("Id").GetValue(administratorDto);
		var name = (string)administratorDtoType.GetProperty("AdministratorName").GetValue(administratorDto);
		var phone = (string)administratorDtoType.GetProperty("AdministratorPhone").GetValue(administratorDto);

		Assert.Equal("Administrator created successfully", message);
		Assert.Equal(administrator.Id, id);
		Assert.Equal("Carlos Clavijo", name);
		Assert.Equal("77601415", phone);
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
		var administratorId = Guid.NewGuid();
		var administratorDto = new AdministratorDto
		{
			Id = administratorId,
			AdministratorName = "Carlos Clavijo",
			AdministratorPhone = "77601415"
		};

		_mediator
			.Setup(m => m.Send(It.Is<GetAdministratorByIdQuery>(q => q.AdministratorId == administratorId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<AdministratorDto>.Success(administratorDto));

		var controller = new AdministratorController(_mediator.Object);
		var result = await controller.GetAdministratorById(administratorId);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var value = okResult.Value;
		var valueType = value.GetType();

		var messageProp = valueType.GetProperty("Message");
		var administratorProp = valueType.GetProperty("Administrator");

		var message = (string)messageProp.GetValue(value);
		var administratorResult = administratorProp.GetValue(value);

		var administratorResultType = administratorResult.GetType();
		var valueProp = administratorResultType.GetProperty("Value");
		var administratorDtoValue = valueProp.GetValue(administratorResult);

		var administratorDtoType = administratorDtoValue.GetType();
		var id = (Guid)administratorDtoType.GetProperty("Id").GetValue(administratorDtoValue);
		var name = (string)administratorDtoType.GetProperty("AdministratorName").GetValue(administratorDtoValue);
		var phone = (string)administratorDtoType.GetProperty("AdministratorPhone").GetValue(administratorDtoValue);

		Assert.NotNull(value);
		Assert.Equal("Administrator details retrieved successfully", message);
		Assert.Equal(administratorId, id);
		Assert.Equal("Carlos Clavijo", name);
		Assert.Equal("77601415", phone);
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

	[Fact]
	public async Task GetAdministratorByIdNullTest()
	{
		var id = Guid.Empty;
		var result = await _controller.GetAdministratorById(id);
		var status = Assert.IsType<NotFoundObjectResult>(result);

		Assert.Equal(StatusCodes.Status404NotFound, status.StatusCode);
	}
}
