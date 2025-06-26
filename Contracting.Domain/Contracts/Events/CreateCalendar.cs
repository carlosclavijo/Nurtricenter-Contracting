using Contracting.Domain.Abstractions;
using Contracting.Domain.Delivery;

namespace Contracting.Domain.Contracts.Events;

public record CreateCalendar(Guid ContractId, Guid PatientId, DateTime StartDate, DateTime EndDate, List<DeliveryDay> DeliveryDays) : DomainEvent;
