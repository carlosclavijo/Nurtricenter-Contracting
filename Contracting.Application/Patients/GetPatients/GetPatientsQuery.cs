using MediatR;

namespace Contracting.Application.Patients.GetPatients;

public record GetPatientsQuery(string SearchTerm) : IRequest<IEnumerable<PatientDto>>;