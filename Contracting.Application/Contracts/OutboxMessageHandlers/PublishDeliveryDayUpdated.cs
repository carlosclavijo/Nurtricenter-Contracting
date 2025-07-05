using Contracting.Domain.Contracts.Events;
using Contracting.Domain.DeliveryDays.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;

namespace Contracting.Application.Contracts.OutboxMessageHandlers;

public class PublishDeliveryDayUpdated(IExternalPublisher integrationBusService) : INotificationHandler<OutboxMessage<DeliveryDayUpdated>>
{
	public async Task Handle(OutboxMessage<DeliveryDayUpdated> notification, CancellationToken cancellationToken)
	{
		DeliberyDelete message = new(
			notification.Content.ContractId,
			notification.Content.DeliveryDayId,
			notification.Content.Street,
			notification.Content.Number,
			notification.CorrelationId,
			"deliveryday"
		);

		await integrationBusService.PublishAsync(message);
	}
}
