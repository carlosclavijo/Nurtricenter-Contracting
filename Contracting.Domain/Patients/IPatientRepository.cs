using System;
using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Patients;

public interface IPatientRepository : IRepository<Patient>
{
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Guid id);

}