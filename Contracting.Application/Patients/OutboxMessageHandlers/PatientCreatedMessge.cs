using Joseco.Communication.External.Contracts.Message;

namespace Contracting.Application.Patients.OutboxMessageHandlers;

public record PatientCreatedMessge(Guid PatientId, string Name, string Phone, string? CorrelationId = null, string? Source = null) : IntegrationMessage(CorrelationId, Source);