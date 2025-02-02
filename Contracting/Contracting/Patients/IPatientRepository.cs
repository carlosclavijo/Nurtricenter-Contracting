using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Patients;

public interface IPatientRepository : IRepository<Patient>
{
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Guid id);

}