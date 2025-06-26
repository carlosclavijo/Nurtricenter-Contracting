using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.WebApi.Controllers;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

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
        var administratorId = Guid.NewGuid();
        var command = new CreateAdministratorCommand("Carlos Clavijo", "77601415");
        var administratorDto = new AdministratorDto
        {
            Id = administratorId,
            AdministratorName = "Carlos Clavijo",
            AdministratorPhone = "77601415"
        };

		_mediator
			.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(administratorId);

		_mediator
			.Setup(m => m.Send(It.Is<GetAdministratorByIdQuery>(q => q.AdministratorId == administratorId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<AdministratorDto>.Success(administratorDto));

        var controller = new AdministratorController(_mediator.Object);
        var result = await controller.CreateAdministrator(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		dynamic response = okResult.Value;
		var responseJson = JObject.FromObject(okResult.Value);

		Assert.Equal("Administrator created successfully", responseJson["Message"]!.ToString());
		var adminJson = responseJson["Administrator"]!;
		Assert.Equal(administratorDto.Id.ToString(), adminJson["Id"]!.ToString());
		Assert.Equal(administratorDto.AdministratorName, adminJson["AdministratorName"]!.ToString());
		Assert.Equal(administratorDto.AdministratorPhone, adminJson["AdministratorPhone"]!.ToString());
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
		var responseJson = JObject.FromObject(okResult.Value);

		Assert.Equal("Administrator details retrieved successfully", responseJson["Message"]!.ToString());

		var adminJson = responseJson["Administrator"]!;
		Assert.Equal(administratorId.ToString(), adminJson["Id"]!.ToString());
		Assert.Equal("Carlos Clavijo", adminJson["AdministratorName"]!.ToString());
		Assert.Equal("77601415", adminJson["AdministratorPhone"]!.ToString());
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
