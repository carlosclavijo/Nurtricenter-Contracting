using Contracting.Application.Patients.GetPatients;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Patients.GetPatientById;

public record GetPatientByIdQuery(Guid PatientId) : IRequest<Result<PatientDto>>;