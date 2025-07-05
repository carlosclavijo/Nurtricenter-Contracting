using Joseco.Communication.External.Contracts.Message;

namespace Contracting.Application.Contracts.OutboxMessageHandlers;

public record DeliberyDelete(Guid ContractId, Guid DeliveryDayId, string? CorrelationId = null, string? Source = null) : IntegrationMessage(CorrelationId, Source);