using Contracting.Application.Contracts.DomainEventHandlers;
using Contracting.Application.Patients.DomainEventHandlers;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients.Events;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using Moq;

namespace Contracting.Test.Application.Patients;

public class SaveInOutboxWhenPatientCreatedTest
{
	public readonly Mock<IOutboxService<DomainEvent>> _outboxService;
	public readonly Mock<IUnitOfWork> _unitOfWork;
	public readonly SaveInOutboxWhenPatienCreated _handler;

	public SaveInOutboxWhenPatientCreatedTest()
	{
		_outboxService = new Mock<IOutboxService<DomainEvent>>();
		_unitOfWork = new Mock<IUnitOfWork>();
		_handler = new SaveInOutboxWhenPatienCreated(_outboxService.Object, _unitOfWork.Object);
	}

	[Fact]
	public async Task HandleIsValid()
	{
		var patientId = Guid.NewGuid();
		var name = "Carlos";
		var phone = "77887878";

		var domainEvent = new PatientCreated(patientId, name, phone);
		var cancellationToken = CancellationToken.None;

		await _handler.Handle(domainEvent, cancellationToken);

		_outboxService.Verify(s => s.AddAsync(
			It.Is<OutboxMessage<DomainEvent>>(msg => msg.Content == domainEvent)), Times.Once);

		_unitOfWork.Verify(u => u.CommitAsync(cancellationToken), Times.Once);
	}
}
