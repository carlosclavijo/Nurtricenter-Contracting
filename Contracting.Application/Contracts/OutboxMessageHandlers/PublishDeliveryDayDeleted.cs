using Contracting.Domain.DeliveryDays.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;

namespace Contracting.Application.Contracts.OutboxMessageHandlers;

public class PublishDeliveryDayDeleted(IExternalPublisher integrationBusService) : INotificationHandler<OutboxMessage<DeliveryDayDeleted>>
{
	public async Task Handle(OutboxMessage<DeliveryDayDeleted> notification, CancellationToken cancellationToken)
	{
		DeliberyDelete message = new(
			notification.Content.ContractId,
			notification.Content.DeliveryDayId,
			notification.CorrelationId,
			"deliveryday"
		);
		await integrationBusService.PublishAsync(message);
	}
}
