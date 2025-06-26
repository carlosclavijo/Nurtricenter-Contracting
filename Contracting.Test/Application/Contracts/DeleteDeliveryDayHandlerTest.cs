using Contracting.Application.Contracts.DeleteDeliveryDay;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class DeleteDeliveryDayHandlerTest
{
	private readonly Mock<IContractRepository> _contractRepository;
	private readonly Mock<IUnitOfWork> _unitOfWork;

	public DeleteDeliveryDayHandlerTest()
	{
		_contractRepository = new Mock<IContractRepository>();
		_unitOfWork = new Mock<IUnitOfWork>();
	}

	[Fact]
    public async Task HandleIsValid()
    {
        var contractId = Guid.NewGuid();
        var deliveryDay = new DeliveryDay(contractId, DateTime.UtcNow, "Main St", 123);
        var contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.UtcNow);
        contract.CreateCalendar(new List<DeliveryDay> { deliveryDay });

        _contractRepository
            .Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>()))
            .ReturnsAsync(contract);

        _unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteDeliveryDayHandler(_contractRepository.Object, _unitOfWork.Object);
        var command = new DeleteDeliveryDayCommand(contractId, deliveryDay.Id);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(contractId, result.Value);
        Assert.DoesNotContain(contract.DeliveryDays, d => d.Id == deliveryDay.Id);

        _unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

 
	[Fact]
	public async Task HandleInvalidWhenContractIsNull()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();

		_contractRepository
			.Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>()))
			.ReturnsAsync((Contract?)null);

		var handler = new DeleteDeliveryDayHandler(_contractRepository.Object, _unitOfWork.Object);
		var command = new DeleteDeliveryDayCommand(contractId, deliveryDayId);
		var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));

		Assert.Equal("Contract not found", ex.Message);
		_unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
	}

	[Fact]
	public async Task Handle_ShouldThrow_WhenDeliveryDayNotFound()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();
		var contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.UtcNow);

		_contractRepository
			.Setup(r => r.GetByIdAsync(contractId, It.IsAny<bool>()))
			.ReturnsAsync(contract);

		var handler = new DeleteDeliveryDayHandler(_contractRepository.Object, _unitOfWork.Object);
		var command = new DeleteDeliveryDayCommand(contractId, deliveryDayId);
		var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));

		Assert.Equal("DeliveryDay not found", ex.Message);
		_unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
	}
}
