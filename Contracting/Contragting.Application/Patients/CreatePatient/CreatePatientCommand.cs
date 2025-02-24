using System;
using MediatR;

namespace Contracting.Application.Patients.CreatePatient;

public record CreatePatientCommand(string PatientName, string PatientPhone) : IRequest<Guid>;