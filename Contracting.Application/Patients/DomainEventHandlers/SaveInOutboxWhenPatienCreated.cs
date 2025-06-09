using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using MediatR;

namespace Contracting.Application.Patients.DomainEventHandlers;

internal class SaveInOutboxWhenPatienCreated(IOutboxService<DomainEvent> outboxService, IUnitOfWork unitOfWork) : INotificationHandler<PatientCreated>
{
	public async Task Handle(PatientCreated domainEvent, CancellationToken cancellationToken)
	{
		OutboxMessage<DomainEvent> outboxMessage = new(domainEvent);

		await outboxService.AddAsync(outboxMessage);
		await unitOfWork.CommitAsync(cancellationToken);
	}
}
