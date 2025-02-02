using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contracting.Application.Patients.CreatePatient;

public record CreatePatientCommand(string PatientName, string PatientPhone) : IRequest<Guid>;