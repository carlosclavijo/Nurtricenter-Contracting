using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Delivery;
using Joseco.Communication.External.Contracts.Message;

namespace Contracting.Application.Contracts.OutboxMessageHandlers;

public record CalendarCreatedMessage(Guid ContractId, Guid PatiendId, DateTime StartTime, DateTime EndDate, List<DeliveryDay> DeliveryDays, string? CorrelationId = null, string? Source = null) : IntegrationMessage(CorrelationId, Source);