using Contracting.Domain.Abstractions;
using Contracting.Domain.DeliveryDays.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using MediatR;

namespace Contracting.Application.Contracts.DomainEventHandlers;

public class SaveInOutboxWhenDeliveryDayUpdated(IOutboxService<DomainEvent> OutboxService, IUnitOfWork UnitOfWork) : INotificationHandler<DeliveryDayUpdated>
{
	public async Task Handle(DeliveryDayUpdated notification, CancellationToken cancellationToken)
	{
		OutboxMessage<DomainEvent> outboxMessage = new(notification);
		await OutboxService.AddAsync(outboxMessage);
		await UnitOfWork.CommitAsync(cancellationToken);
	}
}
