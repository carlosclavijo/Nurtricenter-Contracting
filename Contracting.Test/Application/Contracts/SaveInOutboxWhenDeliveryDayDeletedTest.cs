using Contracting.Application.Contracts.DomainEventHandlers;
using Contracting.Application.Contracts.OutboxMessageHandlers;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts.Events;
using Contracting.Domain.Delivery;
using Contracting.Domain.DeliveryDays.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using Moq;
using System.Reflection.Metadata;

namespace Contracting.Test.Application.Contracts;

public class SaveInOutboxWhenDeliveryDayDeletedTest
{
	public readonly Mock<IOutboxService<DomainEvent>> _outboxService;
	public readonly Mock<IUnitOfWork> _unitOfWork;
	public readonly SaveInOutboxWhenDeliveryDayDeleted _handler;

	public SaveInOutboxWhenDeliveryDayDeletedTest()
	{
		_outboxService = new Mock<IOutboxService<DomainEvent>>();
		_unitOfWork = new Mock<IUnitOfWork>();
		_handler = new SaveInOutboxWhenDeliveryDayDeleted(_outboxService.Object, _unitOfWork.Object);
	}

	[Fact]
	public async Task HandleIsValid()
	{
		var contractId = Guid.NewGuid();
		var deliveryDayId = Guid.NewGuid();
		var domainEvent = new DeliveryDayDeleted(contractId, deliveryDayId);
		var cancellationToken = CancellationToken.None;
		await _handler.Handle(domainEvent, cancellationToken);
		_outboxService.Verify(s => s.AddAsync(It.Is<OutboxMessage<DomainEvent>>(msg => msg.Content == domainEvent)), Times.Once);
		_unitOfWork.Verify(u => u.CommitAsync(cancellationToken), Times.Once);
	}
}