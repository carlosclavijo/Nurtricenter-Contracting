using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using MediatR;

namespace Contracting.Application.Patients.DomainEventHandlers;

public class SaveInOutboxWhenPatienCreated(IOutboxService<DomainEvent> OutboxService, IUnitOfWork UnitOfWork) : INotificationHandler<PatientCreated>
{
	public async Task Handle(PatientCreated domainEvent, CancellationToken cancellationToken)
	{
		OutboxMessage<DomainEvent> outboxMessage = new(domainEvent);

		await OutboxService.AddAsync(outboxMessage);
		await UnitOfWork.CommitAsync(cancellationToken);
	}
}
