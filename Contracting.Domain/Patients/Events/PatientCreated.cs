using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Patients.Events;

public record PatientCreated(Guid PatientId, string Name, string Phone) : DomainEvent;