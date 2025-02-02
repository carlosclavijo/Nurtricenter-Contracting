using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Domain.Contracts;

public class ContractFactory : IContractFactory
{
    public Contract CreateFullMonthContract(Guid administratorId, Guid patientId, DateTime startDate)
    {
        if (administratorId == Guid.Empty)
        {
            throw new ArgumentNullException("AdministratorId is required", nameof(administratorId));
        }
        if (patientId == Guid.Empty)
        {
            throw new ArgumentNullException("PatientId is required", nameof(patientId));
        }
        return new Contract(administratorId, patientId, ContractType.FullMonth, startDate);
    }

    public Contract CreateHalfMonthContract(Guid administratorId, Guid patientId, DateTime startDate)
    {
        if (administratorId == Guid.Empty)
        {
            throw new ArgumentNullException("AdministratorId is required", nameof(administratorId));
        }
        if (patientId == Guid.Empty)
        {
            throw new ArgumentNullException("PatientId is required", nameof(patientId));
        }
        return new Contract(administratorId, patientId, ContractType.HalfMonth, startDate);
    }
}