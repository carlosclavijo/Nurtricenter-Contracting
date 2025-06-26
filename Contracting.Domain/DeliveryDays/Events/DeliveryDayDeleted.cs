using Contracting.Domain.Abstractions;

namespace Contracting.Domain.DeliveryDays.Events;

public record DeliveryDayDeleted(Guid ContractId, Guid DeliveryDayId) : DomainEvent;