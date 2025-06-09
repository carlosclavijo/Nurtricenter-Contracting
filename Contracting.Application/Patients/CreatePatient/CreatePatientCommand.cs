using System;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Patients.CreatePatient;

public record CreatePatientCommand(string PatientName, string PatientPhone) : IRequest<Result<Guid>> { };