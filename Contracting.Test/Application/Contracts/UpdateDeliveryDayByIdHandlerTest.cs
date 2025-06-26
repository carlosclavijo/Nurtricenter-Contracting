using Contracting.Application.Contracts.UpdateDeliveryDayById;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class UpdateDeliveryDayByIdHandlerTest
{
	private readonly Mock<IContractRepository> _contractRepository;
	private readonly Mock<IUnitOfWork> _unitOfWork;

	public UpdateDeliveryDayByIdHandlerTest()
	{
		_contractRepository = new Mock<IContractRepository>();
		_unitOfWork = new Mock<IUnitOfWork>();
	}

	[Fact]
	public async Task UpdateDeliveryDay_ValidRequest_ReturnsSuccess()
	{
		var deliveryDayId = Guid.NewGuid();
		var street = "Updated Street";
		var number = 42;
		var startDate = DateTime.UtcNow.AddDays(1);
		var contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.HalfMonth, startDate);
		var deliveryDay = new DeliveryDay(contract.Id, startDate, "Old Street", 10);
		typeof(DeliveryDay).GetProperty("Id")!.SetValue(deliveryDay, deliveryDayId);
		contract.DeliveryDays.Add(deliveryDay);

		_contractRepository
			.Setup(repo => repo.GetByIdAsync(contract.Id, It.IsAny<bool>()))
			.ReturnsAsync(contract);

		_unitOfWork
			.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
			.Returns(Task.CompletedTask);

		var command = new UpdateDeliveyDayByIdCommand(contract.Id, deliveryDayId, street, number);
		var handler = new UpdateDeliveryDayByIdHandler(_contractRepository.Object, _unitOfWork.Object);
		var result = await handler.Handle(command, CancellationToken.None);
		var updatedDay = contract.DeliveryDays.First(d => d.Id == deliveryDayId);

		Assert.True(result.IsSuccess);
		Assert.Equal(contract.Id, result.Value);
		Assert.Equal(street, updatedDay.Street);
		Assert.Equal(number, updatedDay.Number);

		_contractRepository.Verify(r => r.GetByIdAsync(contract.Id, It.IsAny<bool>()), Times.Once);
		_unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
	}

	[Fact]
	public async Task HAndleInvalidWhenContractIsNull()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();
		var street = "Updated Street";
		var number = 42;
		_contractRepository
			.Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>()))
			.ReturnsAsync((Contract?)null);
		var handler = new UpdateDeliveryDayByIdHandler(_contractRepository.Object, _unitOfWork.Object);
		var command = new UpdateDeliveyDayByIdCommand(contractId, deliveryDayId, street, number);

		await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
	}

	[Fact]
	public async Task HandleInvalidWhenDeliveryDayIsNull()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();
		var street = "Updated Street";
		var number = 42;
		var contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.HalfMonth, DateTime.UtcNow.AddDays(1));
		_contractRepository
			.Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>()))
			.ReturnsAsync(contract);
		var handler = new UpdateDeliveryDayByIdHandler(_contractRepository.Object, _unitOfWork.Object);
		var command = new UpdateDeliveyDayByIdCommand(contractId, deliveryDayId, street, number);
		await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
	}
}
