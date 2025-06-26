using System.Collections.ObjectModel;
using Contracting.Application.Contracts.CompleteContract;
using Contracting.Application.Contracts.CreateContract;
using Contracting.Application.Contracts.DeleteDeliveryDay;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Application.Contracts.GetDeliveryDay;
using Contracting.Application.Contracts.InProgressContract;
using Contracting.Application.Contracts.UpdateDeliveryDayById;
using Contracting.Application.Contracts.UpdateDeliveryDays;
using Contracting.Domain.Contracts;
using Contracting.WebApi.Controllers;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

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

	[Fact]
	public async Task CreateContractValidTest()
	{
		var contractId = Guid.NewGuid();
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var startDate = DateTime.UtcNow;
		var deliveryDays = new List<CreateDeliveryDaysCommand>
		{
			new(startDate.AddDays(1), "Calle 1", 101),
			new(startDate.AddDays(2), "Calle 2", 102)
		};

		var command = new CreateContractCommand(administratorId, patientId, "Mensual", startDate, deliveryDays);
		var contractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = administratorId,
			PatientId = patientId,
			Type = "Mensual",
			Status = "InProgress",
			CreationDate = DateTime.UtcNow,
			StartDate = startDate,
			CompleteDate = startDate.AddMonths(1),
			CostValue = 500,
			DeliveryDays = new List<DeliveryDayDto>
			{
				new() { Id = Guid.NewGuid(), DateTime = startDate.AddDays(1), Street = "Calle 1", Number = 101},
				new() { Id = Guid.NewGuid(), DateTime = startDate.AddDays(2), Street = "Calle 2", Number = 102}
			}
		};

		_mediator
			.Setup(m => m.Send(It.Is<CreateContractCommand>(c => c.AdministratorId == administratorId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<Guid>.Success(contractId));

		_mediator
			.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<ContractDto>.Success(contractDto));

		var controller = new ContractController(_mediator.Object);
		var result = await controller.CreateContract(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var responseJson = JObject.FromObject(okResult.Value);

		Assert.Equal("Contract created successfully", responseJson["Message"]!.ToString());

		var contractJson = responseJson["Contract"]!;
		Assert.Equal(contractId.ToString(), contractJson["Id"]!.ToString());
		Assert.Equal("Mensual", contractJson["Type"]!.ToString());
		Assert.Equal("InProgress", contractJson["Status"]!.ToString());

		var deliveryList = contractJson["DeliveryDays"]!.ToObject<List<JObject>>();
		Assert.Equal(2, deliveryList!.Count);
		Assert.Equal("Calle 1", deliveryList[0]["Street"]!.ToString());
		Assert.Equal("Calle 2", deliveryList[1]["Street"]!.ToString());
	}

	[Fact]
    public async Task CreateContractInvalidTest()
    {
		var startDelivery1 = DateTime.UtcNow.AddDays(2);
		var street1 = "Grove Street";
		var number1 = 30;
		var startDelivery2 = DateTime.UtcNow.AddDays(3);
		var street2 = "Spooner Street";
		var number2 = 30;
		var deliveryCommand1 = new CreateDeliveryDaysCommand(startDelivery1, street1, number1);
		var deliveryCommand2 = new CreateDeliveryDaysCommand(startDelivery2, street2, number2);
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var type = "FullMonth";
		var startDate = DateTime.UtcNow;
		var days = new Collection<CreateDeliveryDaysCommand> { deliveryCommand1, deliveryCommand2 };
		var command = new CreateContractCommand(administratorId, patientId, type, startDate, days);

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
		var contractId = Guid.NewGuid();
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var startDate = new DateTime(2025, 6, 21);

		var contractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = administratorId,
			PatientId = patientId,
			Type = "Mensual",
			Status = "InProgress",
			CreationDate = DateTime.UtcNow,
			StartDate = startDate,
			CompleteDate = startDate.AddMonths(1),
			CostValue = 500,
			DeliveryDays = new List<DeliveryDayDto>
			{
				new() { Id = Guid.NewGuid(), DateTime = startDate.AddDays(1), Street = "Calle 1", Number = 101},
				new() { Id = Guid.NewGuid(), DateTime = startDate.AddDays(2), Street = "Calle 2", Number = 102}
			}
		};

		_mediator
			.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<ContractDto>.Success(contractDto));

		var controller = new ContractController(_mediator.Object);
		var result = await controller.GetContractById(contractId);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var responseJson = JObject.FromObject(okResult.Value);

		Assert.Equal("Contract details retrieved successfully", responseJson["Message"]!.ToString());

		var contractJson = responseJson["Contract"]!;
		Assert.Equal(contractId.ToString(), contractJson["Id"]!.ToString());
		Assert.Equal("Mensual", contractJson["Type"]!.ToString());

		var deliveryList = contractJson["DeliveryDays"]!.ToObject<List<JObject>>();
		Assert.Equal(2, deliveryList!.Count);
		Assert.Equal("Calle 1", deliveryList[0]["Street"]!.ToString());
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
	public async Task GetContractByNullIdTest()
	{
		var id = Guid.Empty;
		var result = await _controller.GetContractById(id);
		var status = Assert.IsType<NotFoundObjectResult>(result);

		Assert.Equal(StatusCodes.Status404NotFound, status.StatusCode);
	}

	[Fact]
    public async Task InProgressContractValidTest()
    {
        var contractId = Guid.NewGuid();
        var command = new InProgressContractCommand(contractId);

        var expectedContractDto = new ContractDto
        {
            Id = contractId,
            AdministratorId = Guid.NewGuid(),
            PatientId = Guid.NewGuid(),
            Type = "HalfMonth",
            Status = "InProgress",
            CreationDate = DateTime.UtcNow,
            StartDate = DateTime.UtcNow.AddDays(1),
            CompleteDate = DateTime.UtcNow.AddDays(15),
            CostValue = 100.0m,
            DeliveryDays = new List<DeliveryDayDto>()
        };

        _mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mediator.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<ContractDto>.Success(expectedContractDto));

        var controller = new ContractController(_mediator.Object);
        var result = await controller.InProgressContract(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var responseObject = okResult.Value;

		var messageProp = responseObject.GetType().GetProperty("Message");
		var message = (string)messageProp.GetValue(responseObject);

		Assert.Equal("Contract is now in progress", message);
    }

	[Fact]
	public async Task InProgressContractInvalidTest()
	{
		var contractId = Guid.NewGuid();
		var command = new InProgressContractCommand(contractId);
		var mediatorMock = new Mock<IMediator>();

		mediatorMock.Setup(m => m.Send(It.IsAny<InProgressContractCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

		var controller = new ContractController(mediatorMock.Object);
		var result = await controller.InProgressContract(command);
		var badRequest = Assert.IsType<BadRequestObjectResult>(result);
		dynamic response = badRequest.Value;

		var messageProp = response.GetType().GetProperty("Message");
		var message = (string)messageProp.GetValue(response);;

		Assert.Equal("Failed to set contract in progress", message);
	}

	[Fact]
	public async Task InProgressContractServerErrorTest()
	{
		var contractId = Guid.NewGuid();
		var command = new InProgressContractCommand(contractId);
		var exceptionMessage = "Unexpected error occurred";

		_mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(exceptionMessage));

		var controller = new ContractController(_mediator.Object);
		var result = await controller.InProgressContract(command);
		var objectResult = Assert.IsType<ObjectResult>(result);

		Assert.Equal(500, objectResult.StatusCode);
		Assert.Equal(exceptionMessage, objectResult.Value);
	}

	[Fact]
	public async Task CompletedContractValidTest()
	{
		var contractId = Guid.NewGuid();
		var command = new CompleteContractCommand(contractId);

		var expectedContractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = Guid.NewGuid(),
			PatientId = Guid.NewGuid(),
			Type = "HalfMonth",
			Status = "Completed",
			CreationDate = DateTime.UtcNow,
			StartDate = DateTime.UtcNow.AddDays(1),
			CompleteDate = DateTime.UtcNow.AddDays(15),
			CostValue = 100.0m,
			DeliveryDays = new List<DeliveryDayDto>()
		};

		_mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);
		_mediator.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<ContractDto>.Success(expectedContractDto));

		var controller = new ContractController(_mediator.Object);
		var result = await controller.CompleteContract(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var responseObject = okResult.Value;

		var messageProp = responseObject.GetType().GetProperty("Message");
		var message = (string)messageProp.GetValue(responseObject);

		Assert.Equal("Contract is now Completed", message);
	}

	[Fact]
	public async Task CompleteContractInvalidTest()
	{
		var contractId = Guid.NewGuid();
		var command = new CompleteContractCommand(contractId);
		var mediatorMock = new Mock<IMediator>();

		mediatorMock.Setup(m => m.Send(It.IsAny<CompleteContractCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

		var controller = new ContractController(mediatorMock.Object);
		var result = await controller.CompleteContract(command);
		var badRequest = Assert.IsType<BadRequestObjectResult>(result);
		dynamic response = badRequest.Value;

		var messageProp = response.GetType().GetProperty("Message");
		var message = (string)messageProp.GetValue(response); ;

		Assert.Equal("Failed to set contract complete", message);
	}

	[Fact]
	public async Task CompleteContractServerErrorTest()
	{
		var contractId = Guid.NewGuid();
		var command = new CompleteContractCommand(contractId);
		var exceptionMessage = "Unexpected error occurred";

		_mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(exceptionMessage));

		var controller = new ContractController(_mediator.Object);
		var result = await controller.CompleteContract(command);
		var objectResult = Assert.IsType<ObjectResult>(result);

		Assert.Equal(500, objectResult.StatusCode);
		Assert.Equal(exceptionMessage, objectResult.Value);
	}

	[Fact]
	public async Task GetDeliveryDayValidTest()
	{
		// Arrange
		var deliveryDayId = Guid.NewGuid();
		var dto = new DeliveryDayDto
		{
			Id = deliveryDayId,
			ContractId = Guid.NewGuid(),
			DateTime = DateTime.UtcNow,
			Street = "Test St",
			Number = 5
		};

		_mediator.Setup(m => m.Send(It.Is<GetDeliveryDayQuery>(q => q.DeliveryDayId == deliveryDayId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<DeliveryDayDto>.Success(dto));

		var result = await _controller.GetDeliveryDay(deliveryDayId);
		var ok = Assert.IsType<OkObjectResult>(result);
		var obj = JObject.FromObject(ok.Value);
		var d = obj["Delivery"]!;

		Assert.Equal("Delivery details retrieved successfully", obj["Message"]!.ToString());
		Assert.Equal(deliveryDayId.ToString(), d["Id"]!.ToString());
		Assert.Equal("Test St", d["Street"]!.ToString());
		Assert.Equal("5", d["Number"]!.ToString());
	}

	[Fact]
	public async Task GetDeliveryDayInvalidNonIdTest()
	{
		var missingId = Guid.NewGuid();
		_mediator.Setup(m => m.Send(It.Is<GetDeliveryDayQuery>(q => q.DeliveryDayId == missingId), It.IsAny<CancellationToken>())).ReturnsAsync((Result<DeliveryDayDto>?)null);

		var result = await _controller.GetDeliveryDay(missingId);
		var notFound = Assert.IsType<NotFoundObjectResult>(result);
		var obj = JObject.FromObject(notFound.Value);

		Assert.Equal("Delivery not found", obj["message"]!.ToString());
	}

	[Fact]
	public async Task GetDeliveryDayInvalidExceptionTest()
	{
		var someId = Guid.NewGuid();
		var errorMsg = "boom!";
		_mediator.Setup(m => m.Send(It.IsAny<GetDeliveryDayQuery>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(errorMsg));
		var result = await _controller.GetDeliveryDay(someId);
		var objRes = Assert.IsType<ObjectResult>(result);

		Assert.Equal(500, objRes.StatusCode);
		Assert.Equal(errorMsg, objRes.Value);
	}

	[Fact]
	public async Task UpdateDeliveryDays_ValidCommand_ReturnsOk()
	{
		// Arrange
		var contractId = Guid.NewGuid();
		var command = new UpdateDeliveryDaysCommand(
			ContractId: contractId,
			FirstDate: DateTime.UtcNow.Date,
			LastDate: DateTime.UtcNow.Date.AddDays(2),
			Street: "New St",
			Number: 77
		);

		var contractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = Guid.NewGuid(),
			PatientId = Guid.NewGuid(),
			Type = "HalfMonth",
			Status = "InProgress",
			CreationDate = DateTime.UtcNow,
			StartDate = command.FirstDate,
			CompleteDate = command.LastDate,
			CostValue = 100m,
			DeliveryDays = new List<DeliveryDayDto>()
		};

		_mediator.Setup(m => m.Send(It.Is<UpdateDeliveryDaysCommand>(c => c.ContractId == contractId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<Guid>.Success(contractId));
		_mediator.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<ContractDto>.Success(contractDto));

		var result = await _controller.UpdateDeliveryDays(command);
		var ok = Assert.IsType<OkObjectResult>(result);
		var obj = JObject.FromObject(ok.Value);
		var contractJson = obj["Contract"]!;

		Assert.Equal("Addresses changed successfully", obj["Message"]!.ToString());
		Assert.Equal(contractId.ToString(), contractJson["Id"]!.ToString());
		Assert.Equal("InProgress", contractJson["Status"]!.ToString());
	}

	[Fact]
	public async Task UpdateDeliveryDays_WhenExceptionThrown_Returns500()
	{
		var command = new UpdateDeliveryDaysCommand(
			ContractId: Guid.NewGuid(),
			FirstDate: DateTime.UtcNow,
			LastDate: DateTime.UtcNow,
			Street: "X",
			Number: 1
		);
		var errorMessage = "update failed";

		_mediator.Setup(m => m.Send(It.IsAny<UpdateDeliveryDaysCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(errorMessage));

		var result = await _controller.UpdateDeliveryDays(command);
		var objRes = Assert.IsType<ObjectResult>(result);

		Assert.Equal(500, objRes.StatusCode);
		Assert.Equal(errorMessage, objRes.Value);
	}

	[Fact]
	public async Task UpdateDeliveryDayValidTest()
	{
		var contractId = Guid.NewGuid();
		var command = new UpdateDeliveyDayByIdCommand(contractId, Guid.NewGuid(), "Nicolas Suarez", 30);

		var contractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = Guid.NewGuid(),
			PatientId = Guid.NewGuid(),
			Type = "HalfMonth",
			Status = "InProgress",
			CreationDate = DateTime.UtcNow,
			StartDate = DateTime.UtcNow,
			CompleteDate = DateTime.UtcNow.AddDays(15),
			CostValue = 0m,
			DeliveryDays = new List<DeliveryDayDto>()
		};

		_mediator.Setup(m => m.Send(It.Is<UpdateDeliveyDayByIdCommand>(c => c.ContractId == contractId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<Guid>.Success(contractId));
		_mediator.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId),It.IsAny<CancellationToken>())).ReturnsAsync(Result<ContractDto>.Success(contractDto));

		var result = await _controller.UpdateDeliveryDay(command);
		var ok = Assert.IsType<OkObjectResult>(result);
		var obj = JObject.FromObject(ok.Value);
		var contractJson = obj["Contract"]!;

		Assert.Equal("Address changed successfully", obj["Message"]!.ToString());
		Assert.Equal(contractId.ToString(), contractJson["Id"]!.ToString());
	}

	[Fact]
	public async Task UpdateDeliveryDayInvalidTest()
	{
		var command = new UpdateDeliveyDayByIdCommand(Guid.NewGuid(), Guid.NewGuid(), "X", 1);
		var errorMessage = "update failed";

		_mediator.Setup(m => m.Send(It.IsAny<UpdateDeliveyDayByIdCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(errorMessage));

		var result = await _controller.UpdateDeliveryDay(command);
		var objRes = Assert.IsType<ObjectResult>(result);

		Assert.Equal(500, objRes.StatusCode);
		Assert.Equal(errorMessage, objRes.Value);
	}

	[Fact]
	public async Task DeleteDeliveryDayValidTest()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();
		var command = new DeleteDeliveryDayCommand(contractId, deliveryDayId);

		var contractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = Guid.NewGuid(),
			PatientId = Guid.NewGuid(),
			Type = "HalfMonth",
			Status = "InProgress",
			CreationDate = DateTime.UtcNow,
			StartDate = DateTime.UtcNow,
			CompleteDate = DateTime.UtcNow.AddDays(15),
			CostValue = 0m,
			DeliveryDays = new List<DeliveryDayDto>()
		};

		_mediator.Setup(m => m.Send(It.Is<DeleteDeliveryDayCommand>(c => c.ContractId == contractId && c.DeliveryDayId == deliveryDayId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<Guid>.Success(contractId));

		_mediator.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>())).ReturnsAsync(Result<ContractDto>.Success(contractDto));

		var result = await _controller.DeleteDeliveryDay(command);
		var ok = Assert.IsType<OkObjectResult>(result);
		var obj = JObject.FromObject(ok.Value);
		var contractJson = obj["Contract"]!;

		Assert.Equal("Delivery day deleted successfully", obj["Message"]!.ToString());
		Assert.Equal(contractId.ToString(), contractJson["Id"]!.ToString());
		Assert.Equal("InProgress", contractJson["Status"]!.ToString());
	}

	[Fact]
	public async Task DeleteDeliveryDay_WhenExceptionThrown_Returns500()
	{
		var command = new DeleteDeliveryDayCommand(Guid.NewGuid(), Guid.NewGuid());
		var errorMessage = "delete failed";

		_mediator.Setup(m => m.Send(It.IsAny<DeleteDeliveryDayCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(errorMessage));

		var result = await _controller.DeleteDeliveryDay(command);
		var objRes = Assert.IsType<ObjectResult>(result);

		Assert.Equal(500, objRes.StatusCode);
		Assert.Equal(errorMessage, objRes.Value);
	}
}
