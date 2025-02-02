using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Domain.Contracts;

public interface IContractFactory
{
    Contract CreateHalfMonthContract(Guid administratorId, Guid patientId, DateTime startDate);
    Contract CreateFullMonthContract(Guid administratorId, Guid patientId, DateTime startDate);
}