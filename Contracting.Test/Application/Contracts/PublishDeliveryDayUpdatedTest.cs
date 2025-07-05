using Contracting.Application.Contracts.OutboxMessageHandlers;
using Contracting.Domain.DeliveryDays.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class PublishDeliveryDayUpdatedTest
{
    private readonly Mock<IExternalPublisher> _integrationBusService;
    private readonly PublishDeliveryDayUpdated _handler;

    public PublishDeliveryDayUpdatedTest()
    {
        _integrationBusService = new Mock<IExternalPublisher>();
        _handler = new PublishDeliveryDayUpdated(_integrationBusService.Object);
    }

    [Fact]
    public async Task Handle_ValidMessage_PublishesDeliveryDayUpdated()
    {
        var contractId = Guid.NewGuid();
        var deliveryDayId = Guid.NewGuid();
        var street = "Main Street";
        var number = 42;
        var correlationId = Guid.NewGuid().ToString();

        var domainEvent = new DeliveryDayUpdated(contractId, deliveryDayId, street, number);
        var outboxMessage = new OutboxMessage<DeliveryDayUpdated>(domainEvent)
        {
            CorrelationId = correlationId
        };

		var cancellationToken = CancellationToken.None;
		DeliberyDelete? publishedMessage = null;

		_integrationBusService
			.Setup(p => p.PublishAsync(It.IsAny<DeliberyDelete>(), It.IsAny<string?>(), It.IsAny<bool>()))
			.Callback<DeliberyDelete, string?, bool>((msg, _, _) => publishedMessage = msg)
			.Returns(Task.CompletedTask);

		await _handler.Handle(outboxMessage, CancellationToken.None);

        Assert.NotNull(publishedMessage);
        Assert.Equal(contractId, publishedMessage.ContractId);
        Assert.Equal(deliveryDayId, publishedMessage.DeliveryDayId);
        Assert.Equal(street, publishedMessage.Street);
        Assert.Equal(number, publishedMessage.Number);
        Assert.Equal(correlationId, publishedMessage.CorrelationId);
        Assert.Equal("deliveryday", publishedMessage.Source);
    }
}
