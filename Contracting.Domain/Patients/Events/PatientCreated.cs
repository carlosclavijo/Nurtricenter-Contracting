using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Patients.Events;

public record PatientCreated : DomainEvent
{
	public Guid PatientId { get; init; }
	public string Name { get; init; }
	public string Phone { get; init; }

	public PatientCreated(Guid patientId, string name, string phone)
	{
		PatientId = patientId;
		Name = name;
		Phone = phone;
	}
}
