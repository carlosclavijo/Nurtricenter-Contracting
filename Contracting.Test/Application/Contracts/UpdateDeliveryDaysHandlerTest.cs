using Contracting.Application.Contracts.UpdateDeliveryDays;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class UpdateDeliveryDaysHandlerTest
{
	private readonly Mock<IContractRepository> _contractRepository;
	private readonly Mock<IUnitOfWork> _unitOfWork;
	private readonly UpdateDeliveryDaysHandler _handler;

	public UpdateDeliveryDaysHandlerTest()
	{
		_contractRepository = new Mock<IContractRepository>();
		_unitOfWork = new Mock<IUnitOfWork>();
		_handler = new UpdateDeliveryDaysHandler(_contractRepository.Object, _unitOfWork.Object);
	}

	[Fact]
	public async Task Handle_ValidRequest_UpdatesDeliveryDaysAndReturnsSuccess()
	{
		var contractId = Guid.NewGuid();
		var startDate = DateTime.UtcNow.Date;
		var endDate = startDate.AddDays(3);
		var street = "Ballivian";
		var number = 36;
		var contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.HalfMonth, startDate);

		var inRange1 = new DeliveryDay(contractId, startDate.AddDays(1), "Old St", 1);
		var inRange2 = new DeliveryDay(contractId, startDate.AddDays(2), "Old St", 2);
		var outOfRange = new DeliveryDay(contractId, startDate.AddDays(5), "Old St", 3);

		contract.DeliveryDays.Add(inRange1);
		contract.DeliveryDays.Add(inRange2);
		contract.DeliveryDays.Add(outOfRange);

		var command = new UpdateDeliveryDaysCommand(contractId, startDate, endDate, street, number);

		_contractRepository.Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>()))
			.ReturnsAsync(contract);

		_unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
			.Returns(Task.CompletedTask);

		var result = await _handler.Handle(command, CancellationToken.None);

		Assert.True(result.IsSuccess);
		Assert.Equal(contract.Id, result.Value);
		Assert.Equal("Ballivian", inRange1.Street);
		Assert.Equal(36, inRange1.Number);
	}

	[Fact]
	public async Task Handle_ContractNotFound_ThrowsException()
	{
		var command = new UpdateDeliveryDaysCommand(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(1), "Manuel Ignacio", 10);

		_contractRepository.Setup(r => r.GetByIdAsync(command.ContractId, It.IsAny<bool>())).ReturnsAsync((Contract?)null);

		var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));

		Assert.Equal("Contract not found", ex.Message);
	}

	[Fact]
	public async Task Handle_NoDeliveryDaysInRange_ThrowsException()
	{
		var contractId = Guid.NewGuid();
		var contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.HalfMonth, DateTime.UtcNow);
		contract.DeliveryDays.Add(new DeliveryDay(contractId, DateTime.UtcNow.AddDays(10), "X", 1));
		var command = new UpdateDeliveryDaysCommand(contractId, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), "Mercado", 50);
		_contractRepository.Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>())).ReturnsAsync(contract);
		var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
		Assert.Equal("No delivery days found in the specified range.", ex.Message);
	}
}