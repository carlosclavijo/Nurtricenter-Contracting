using Contracting.Domain.Contracts;
using Contracting.Infrastructure.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence.Repositories;

public class ContractRepository(DomainDbContext DbContext) : IContractRepository
{
	public async Task AddSync(Contract entity)
	{
		await DbContext.Contract.AddAsync(entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		var obj = await GetByIdAsync(id);
		if (obj != null)
		{
			DbContext.Contract.Remove(obj);
		}
	}

	public async Task<Contract?> GetByIdAsync(Guid id, bool readOnly = false)
	{
		return await DbContext.Contract.Include(c => c.DeliveryDays).FirstOrDefaultAsync(i => i.Id == id);
	}

	public Task UpdateAsync(Contract contract)
	{
		DbContext.Contract.Update(contract);
		return Task.CompletedTask;
	}
}
