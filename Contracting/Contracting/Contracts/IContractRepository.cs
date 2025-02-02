using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Contracts;

public interface IContractRepository : IRepository<Contract>
{
    Task UpdateAsync(Contract contract);
    Task DeleteAsync(Guid id);
}
