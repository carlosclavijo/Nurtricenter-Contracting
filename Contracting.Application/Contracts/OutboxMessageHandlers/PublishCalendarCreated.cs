using Contracting.Domain.Contracts.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;

namespace Contracting.Application.Contracts.OutboxMessageHandlers;

public class PublishCalendarCreated(IExternalPublisher integrationBusService) : INotificationHandler<OutboxMessage<CalendarCreated>>
{
	public async Task Handle(OutboxMessage<CalendarCreated> notification, CancellationToken cancellationToken)
	{
		CalendarCreatedMessage message = new(
			notification.Content.ContractId,
			notification.Content.PatientId,
			notification.Content.StartDate,
			notification.Content.EndDate,
			notification.Content.DeliveryDays,
			notification.CorrelationId,
			"calendar"
		);
		await integrationBusService.PublishAsync(message);
	}
}
