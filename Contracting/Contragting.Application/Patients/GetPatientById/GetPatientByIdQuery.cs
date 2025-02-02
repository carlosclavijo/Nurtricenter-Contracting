using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Patients.GetPatients;
using MediatR;

namespace Contracting.Application.Patients.GetPatientById;

public record GetPatientByIdQuery(Guid PatientId) : IRequest<PatientDto>;