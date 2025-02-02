using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Patients;

public interface IPatienteFactory
{
    Patient Create(string patientName, string patientPhone);
}

