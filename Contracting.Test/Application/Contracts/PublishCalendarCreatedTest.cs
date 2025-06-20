using Contracting.Application.Contracts.OutboxMessageHandlers;
using Contracting.Domain.Contracts.Events;
using Contracting.Domain.Delivery;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class PublishCalendarCreatedTest
{
	private readonly Mock<IExternalPublisher> _integrationBusService;
	private readonly PublishCalendarCreated _handler;

	public PublishCalendarCreatedTest()
	{
		_integrationBusService = new Mock<IExternalPublisher>();
		_handler = new PublishCalendarCreated(_integrationBusService.Object);
	}

	[Fact]
	public async Task HandleIsValid()
	{
		var contractId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var startDate = DateTime.Now;
		var endDate = startDate.AddDays(30);
		var deliveryDays = new List<DeliveryDay>
		{
		   new(contractId, startDate, "Grover Street", 10, 1.2563, -8.2453),
		   new(contractId, startDate, "Elm Street", 10, 8.5823, 8.2234),
		   new(contractId, startDate, "Sesame Street", 10, 17.3810, -3.6432),
		};
		var correlationId = Guid.NewGuid();

		var domainEvent = new CalendarCreated(contractId, patientId, startDate, endDate, deliveryDays);
		var outboxMessage = new OutboxMessage<CalendarCreated>(domainEvent)
		{
			CorrelationId = correlationId.ToString()
		};

		var cancellationToken = CancellationToken.None;

		CalendarCreatedMessage? publishedMessage = null;

		_integrationBusService
			.Setup(p => p.PublishAsync(It.IsAny<CalendarCreatedMessage>(), It.IsAny<string?>(), It.IsAny<bool>()))
			.Callback<CalendarCreatedMessage, string?, bool>((msg, _, _) => publishedMessage = msg)
			.Returns(Task.CompletedTask);

		await _handler.Handle(outboxMessage, cancellationToken);

		Assert.NotNull(publishedMessage);
		Assert.Equal(contractId, publishedMessage.ContractId);
		Assert.Equal(patientId, publishedMessage.PatiendId); // typo original en record
		Assert.Equal(startDate, publishedMessage.StartTime);
		Assert.Equal(endDate, publishedMessage.EndDate);
		Assert.Equal(deliveryDays, publishedMessage.DeliveryDays);
		Assert.Equal(correlationId.ToString(), publishedMessage.CorrelationId);
		Assert.Equal("calendar", publishedMessage.Source);
	}
}
