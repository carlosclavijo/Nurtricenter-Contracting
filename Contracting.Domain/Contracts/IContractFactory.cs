using System;

namespace Contracting.Domain.Contracts;

public interface IContractFactory
{
    Contract CreateHalfMonthContract(Guid administratorId, Guid patientId, DateTime startDate);
    Contract CreateFullMonthContract(Guid administratorId, Guid patientId, DateTime startDate);
}  