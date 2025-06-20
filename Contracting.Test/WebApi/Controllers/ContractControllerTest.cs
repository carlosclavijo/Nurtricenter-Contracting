using System.Collections.ObjectModel;
using Contracting.Application.Contracts.CreateContract;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Domain.Contracts;
using Contracting.WebApi.Controllers;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
		var mediatorMock = new Mock<IMediator>();

		var contractId = Guid.NewGuid();
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var startDate = DateTime.Today;

		var command = new CreateContractCommand(
			administratorId,
			patientId,
			"HalfMonth",
			startDate,
			new List<CreateDeliveryDaysCommand>
			{
			new(startDate.AddDays(1), "Street 1", 100, -63.1, -17.4),
			new(startDate.AddDays(2), "Street 2", 101, -63.2, -17.5)
			}
		);

		mediatorMock
			.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<Guid>.Success(contractId));

		var expectedContractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = administratorId,
			PatientId = patientId,
			Type = "Standard",
			Status = "Created",
			CreationDate = startDate,
			StartDate = startDate,
			CompleteDate = startDate.AddDays(7),
			CostValue = 1500m,
			DeliveryDays = new List<DeliveryDayDto>
			{
				new() { DateTime = startDate.AddDays(1), Street = "Street 1", Number = 100, Longitude = -63.1, Latitude = -17.4 },
				new() { DateTime = startDate.AddDays(2), Street = "Street 2", Number = 101, Longitude = -63.2, Latitude = -17.5 }
			}
		};

		mediatorMock
			.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<ContractDto>.Success(expectedContractDto));

		var controller = new ContractController(mediatorMock.Object);
		var result = await controller.CreateContract(command);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var value = okResult.Value;
		var contractDto = Assert.IsAssignableFrom<Result<ContractDto>>(value);
		var contract = contractDto.Value;

		Assert.Equal(expectedContractDto.Id, contract.Id);
		Assert.Equal(expectedContractDto.AdministratorId, contract.AdministratorId);
		Assert.Equal(expectedContractDto.PatientId, contract.PatientId);
		Assert.Equal(expectedContractDto.Type, contract.Type);
		Assert.Equal(expectedContractDto.Status, contract.Status);
		Assert.Equal(expectedContractDto.CreationDate, contract.CreationDate);
		Assert.Equal(expectedContractDto.StartDate, contract.StartDate);
		Assert.Equal(expectedContractDto.CompleteDate, contract.CompleteDate);
		Assert.Equal(expectedContractDto.CostValue, contract.CostValue);
		Assert.Equal(expectedContractDto.DeliveryDays.Count(), contract.DeliveryDays.Count());
	}

	[Fact]
    public async Task CreateContractInvalidTest()
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
		var mediatorMock = new Mock<IMediator>();

		var contractId = Guid.NewGuid();
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var startDate = DateTime.Today;

		var contractDto = new ContractDto
		{
			Id = contractId,
			AdministratorId = administratorId,
			PatientId = patientId,
			Type = "Standard",
			Status = "Created",
			CreationDate = startDate,
			StartDate = startDate,
			CompleteDate = startDate.AddDays(7),
			CostValue = 1500m,
			DeliveryDays = new List<DeliveryDayDto>
		{
			new() { DateTime = startDate.AddDays(1), Street = "Street 1", Number = 100, Longitude = -63.1, Latitude = -17.4 },
			new() { DateTime = startDate.AddDays(2), Street = "Street 2", Number = 101, Longitude = -63.2, Latitude = -17.5 }
		}
		};

		mediatorMock
			.Setup(m => m.Send(It.Is<GetContractByIdQuery>(q => q.ContractId == contractId), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result<ContractDto>.Success(contractDto));

		var controller = new ContractController(mediatorMock.Object);
		var result = await controller.GetContractById(contractId);
		var okResult = Assert.IsType<OkObjectResult>(result);
		var value = okResult.Value;
		var valueType = value.GetType();
		var messageProp = valueType.GetProperty("Message");
		var contractProp = valueType.GetProperty("Contract");
		var message = (string)messageProp.GetValue(value);
		var contractResult = contractProp.GetValue(value);
		var contractResultType = contractResult.GetType();
		var contractValueProp = contractResultType.GetProperty("Value");
		var contract = contractValueProp.GetValue(contractResult);

		var contractDtoType = contract.GetType();
		var id = (Guid)contractDtoType.GetProperty("Id").GetValue(contract);
		var adminId = (Guid)contractDtoType.GetProperty("AdministratorId").GetValue(contract);
		var patient = (Guid)contractDtoType.GetProperty("PatientId").GetValue(contract);
		var type = (string)contractDtoType.GetProperty("Type").GetValue(contract);
		var status = (string)contractDtoType.GetProperty("Status").GetValue(contract);
		var creationDate = (DateTime)contractDtoType.GetProperty("CreationDate").GetValue(contract);
		var start = (DateTime)contractDtoType.GetProperty("StartDate").GetValue(contract);
		var complete = (DateTime)contractDtoType.GetProperty("CompleteDate").GetValue(contract);
		var cost = (decimal)contractDtoType.GetProperty("CostValue").GetValue(contract);
		var deliveryDays = (IEnumerable<DeliveryDayDto>)contractDtoType.GetProperty("DeliveryDays").GetValue(contract);

		Assert.Equal("Contract details retrieved successfully", message);
		Assert.Equal(contractDto.Id, id);
		Assert.Equal(contractDto.AdministratorId, adminId);
		Assert.Equal(contractDto.PatientId, patient);
		Assert.Equal(contractDto.Type, type);
		Assert.Equal(contractDto.Status, status);
		Assert.Equal(contractDto.CreationDate, creationDate);
		Assert.Equal(contractDto.StartDate, start);
		Assert.Equal(contractDto.CompleteDate, complete);
		Assert.Equal(contractDto.CostValue, cost);
		Assert.Equal(contractDto.DeliveryDays.Count(), deliveryDays.Count());
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
}
