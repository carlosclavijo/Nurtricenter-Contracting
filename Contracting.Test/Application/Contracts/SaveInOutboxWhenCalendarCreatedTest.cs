using Contracting.Application.Contracts.DomainEventHandlers;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts.Events;
using Contracting.Domain.Delivery;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class SaveInOutboxWhenCalendarCreatedTest
{
	public readonly Mock<IOutboxService<DomainEvent>> _outboxService;
	public readonly Mock<IUnitOfWork> _unitOfWork;
	public readonly SaveInOutboxWhenCalendarCreated _handler;

	public SaveInOutboxWhenCalendarCreatedTest()
	{
		_outboxService = new Mock<IOutboxService<DomainEvent>>();
		_unitOfWork = new Mock<IUnitOfWork>();
		_handler = new SaveInOutboxWhenCalendarCreated(_outboxService.Object, _unitOfWork.Object);
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

		var domainEvent = new CalendarCreated(contractId, patientId, startDate, endDate, deliveryDays);
		var cancellationToken = CancellationToken.None;

		await _handler.Handle(domainEvent, cancellationToken);

		_outboxService.Verify(s => s.AddAsync(
			It.Is<OutboxMessage<DomainEvent>>(msg => msg.Content == domainEvent)), Times.Once);

		_unitOfWork.Verify(u => u.CommitAsync(cancellationToken), Times.Once);

	}
}