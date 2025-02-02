using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Administrators;
using Contracting.Infrastructure.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Repositories;

internal class AdministratorRepository : IAdministratorRepository
{
    private readonly DomainDbContext _dbContext;

    public AdministratorRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSync(Administrator entity)
    {
        await _dbContext.Administrator.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var obj = await GetByIdAsync(id);
        _dbContext.Administrator.Remove(obj);
    }

    public async Task<Administrator?> GetByIdAsync(Guid id, bool readOnly = false)
    {
        if (readOnly)
        {
            return await _dbContext.Administrator.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        else
        {
            return await _dbContext.Administrator.FindAsync(id);
        }

    }

    public Task UpdateAsync(Administrator administrador)
    {
        _dbContext.Administrator.Update(administrador);
        return Task.CompletedTask;
    }
}
