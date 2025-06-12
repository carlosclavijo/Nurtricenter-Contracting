using Contracting.Domain.Administrators;
using Contracting.Infrastructure.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence.Repositories;

public class AdministratorRepository(DomainDbContext DbContext) : IAdministratorRepository
{
	public async Task AddSync(Administrator entity)
	{
		await DbContext.Administrator.AddAsync(entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		var obj = await GetByIdAsync(id);
		DbContext.Administrator.Remove(obj);
	}

	public async Task<Administrator?> GetByIdAsync(Guid id, bool readOnly = false)
	{
		if (readOnly)
		{
			return await DbContext.Administrator.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}
		else
		{
			return await DbContext.Administrator.FindAsync(id);
		}
	}

	public Task UpdateAsync(Administrator administrador)
	{
		DbContext.Administrator.Update(administrador);
		return Task.CompletedTask;
	}
}
