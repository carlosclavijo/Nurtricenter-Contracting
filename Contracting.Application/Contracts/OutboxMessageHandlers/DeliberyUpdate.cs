﻿using Joseco.Communication.External.Contracts.Message;

namespace Contracting.Application.Contracts.OutboxMessageHandlers;

public record DeliberyUpdate(Guid ContractId,Guid DeliveryDayId, string Street, int Number, string? CorrelationId = null, string? Source = null) : IntegrationMessage(CorrelationId, Source);