using Contracting.Application.Contracts.OutboxMessageHandlers;
using Contracting.Domain.DeliveryDays.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class PublishDeliveryDayDeletedTest
{
    private readonly Mock<IExternalPublisher> _integrationBusService;
    private readonly PublishDeliveryDayDeleted _handler;

    public PublishDeliveryDayDeletedTest()
    {
        _integrationBusService = new Mock<IExternalPublisher>();
        _handler = new PublishDeliveryDayDeleted(_integrationBusService.Object);
    }

    [Fact]
    public async Task Handle_ValidMessage_PublishesDeliveryDayUpdated()
    {
        var contractId = Guid.NewGuid();
        var deliveryDayId = Guid.NewGuid();
        var correlationId = Guid.NewGuid().ToString();

        var domainEvent = new DeliveryDayDeleted(contractId, deliveryDayId);
        var outboxMessage = new OutboxMessage<DeliveryDayDeleted>(domainEvent)
        {
            CorrelationId = correlationId
        };

		var cancellationToken = CancellationToken.None;
		DeliveryDayDeletedMessage? publishedMessage = null;

		_integrationBusService
			.Setup(p => p.PublishAsync(It.IsAny<DeliveryDayDeletedMessage>(), It.IsAny<string?>(), It.IsAny<bool>()))
			.Callback<DeliveryDayDeletedMessage, string?, bool>((msg, _, _) => publishedMessage = msg)
			.Returns(Task.CompletedTask);

		await _handler.Handle(outboxMessage, CancellationToken.None);

        Assert.NotNull(publishedMessage);
        Assert.Equal(contractId, publishedMessage.ContractId);
        Assert.Equal(deliveryDayId, publishedMessage.DeliveryDayId);
        Assert.Equal(correlationId, publishedMessage.CorrelationId);
        Assert.Equal("deliveryday", publishedMessage.Source);
    }
}
