using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using MediatR;

namespace Contracting.Application.Contracts.DomainEventHandlers;

public class SaveInOutboxWhenCalendarCreated(IOutboxService<DomainEvent> OutboxService, IUnitOfWork UnitOfWork) : INotificationHandler<CreateCalendar>
{
	public async Task Handle(CreateCalendar domainEvent, CancellationToken cancellationToken)
	{
		OutboxMessage<DomainEvent> outboxMessage = new(domainEvent);
		await OutboxService.AddAsync(outboxMessage);
		await UnitOfWork.CommitAsync(cancellationToken);
	}
}