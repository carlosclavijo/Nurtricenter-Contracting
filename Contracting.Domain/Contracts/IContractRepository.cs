using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Contracts;

public interface IContractRepository : IRepository<Contract>
{
    Task UpdateAsync(Contract contract);
    Task DeleteAsync(Guid id);
}
