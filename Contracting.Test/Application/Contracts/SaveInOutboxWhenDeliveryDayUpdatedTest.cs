using Contracting.Application.Contracts.DomainEventHandlers;
using Contracting.Domain.Abstractions;
using Contracting.Domain.DeliveryDays.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class SaveInOutboxWhenDeliveryDayUpdatedTest
{
	public readonly Mock<IOutboxService<DomainEvent>> _outboxService;
	public readonly Mock<IUnitOfWork> _unitOfWork;
	public readonly SaveInOutboxWhenDeliveryDayUpdated _handler;

	public SaveInOutboxWhenDeliveryDayUpdatedTest()
	{
		_outboxService = new Mock<IOutboxService<DomainEvent>>();
		_unitOfWork = new Mock<IUnitOfWork>();
		_handler = new SaveInOutboxWhenDeliveryDayUpdated(_outboxService.Object, _unitOfWork.Object);
	}

	[Fact]
	public async Task HandleIsValid()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();
		var domainEvent = new DeliveryDayUpdated(contractId, deliveryDayId, "Avaroa", 100);
		var cancellationToken = CancellationToken.None;
		await _handler.Handle(domainEvent, cancellationToken);
		_outboxService.Verify(s => s.AddAsync(It.Is<OutboxMessage<DomainEvent>>(msg => msg.Content == domainEvent)), Times.Once);
		_unitOfWork.Verify(u => u.CommitAsync(cancellationToken), Times.Once);
	}
}
