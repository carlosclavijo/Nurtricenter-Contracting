using Contracting.Domain.Abstractions;

namespace Contracting.Domain.DeliveryDays.Events;

public record class DeliveryDayUpdated(Guid ContractId, Guid DeliveryDayId, string Street, int Number) : DomainEvent;