using Contracting.Domain.Patients.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;

namespace Contracting.Application.Patients.OutboxMessageHandlers;

class PublishProductCreated(IExternalPublisher integrationBusService) : INotificationHandler<OutboxMessage<PatientCreated>>
{
	public async Task Handle(OutboxMessage<PatientCreated> notification, CancellationToken canellationToken)
	{
		PatientCreatedMessge message = new(
			notification.Content.PatientId,
			notification.Content.Name,
			notification.Content.Phone,
			notification.CorrelationId,
			"patient"
		);

		await integrationBusService.PublishAsync(message);
	}
}
