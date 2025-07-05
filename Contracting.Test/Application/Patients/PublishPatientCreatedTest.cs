using Contracting.Application.Patients.OutboxMessageHandlers;
using Contracting.Domain.Patients.Events;
using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using Moq;

namespace Contracting.Test.Application.Patients;

public class PublishPatientCreatedTest
{
	private readonly Mock<IExternalPublisher> _integrationBusService;
	private readonly PublishProductCreated _handler;

	public PublishPatientCreatedTest()
	{
		_integrationBusService = new Mock<IExternalPublisher>();
		_handler = new PublishProductCreated(_integrationBusService.Object);
	}

	[Fact]
	public async Task HandleIsValid()
	{
		var patientId = Guid.NewGuid();
		var name = "Carlos Clavijo";
		var phone = "7092048";
		var correlationId = Guid.NewGuid().ToString();

		var domainEvent = new PatientCreated(patientId, name, phone);
		var outboxMessage = new OutboxMessage<PatientCreated>(domainEvent)
		{
			CorrelationId = correlationId
		};

		var cancellationToken = CancellationToken.None;
		PacienteCreado? publishedMessage = null;

		_integrationBusService
			.Setup(p => p.PublishAsync(It.IsAny<PacienteCreado>(), It.IsAny<string?>(), It.IsAny<bool>()))
			.Callback<PacienteCreado, string?, bool>((msg, _, _) => publishedMessage = msg)
			.Returns(Task.CompletedTask);

		await _handler.Handle(outboxMessage, cancellationToken);

		Assert.NotNull(publishedMessage);
		Assert.Equal(patientId, publishedMessage.PatientId);
		Assert.Equal(name, publishedMessage.Name);
		Assert.Equal(phone, publishedMessage.Phone);
		Assert.Equal(correlationId, publishedMessage.CorrelationId);
		Assert.Equal("patient", publishedMessage.Source);
	}
}
