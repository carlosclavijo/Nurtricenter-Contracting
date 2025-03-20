using System;
using Contracting.Application.Patients.GetPatients;
using MediatR;

namespace Contracting.Application.Patients.GetPatientById;

public record GetPatientByIdQuery(Guid PatientId) : IRequest<PatientDto>;