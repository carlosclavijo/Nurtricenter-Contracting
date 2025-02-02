using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Contracts;
using Contracting.Infrastructure.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Repositories;

internal class ContractRepository : IContractRepository
{
    private readonly DomainDbContext _dbContext;

    public ContractRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSync(Contract entity)
    {
        await _dbContext.Contract.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var obj = await GetByIdAsync(id);
        if (obj != null)
        {
            _dbContext.Contract.Remove(obj);
        }
    }

    public async Task<Contract?> GetByIdAsync(Guid id, bool readOnly = false)
    {
        return await _dbContext.Contract.Include(c => c.DeliveryDays).FirstOrDefaultAsync(i => i.Id == id);
    }

    public Task UpdateAsync(Contract contract)
    {
        _dbContext.Contract.Update(contract);
        return Task.CompletedTask;
    }
}
