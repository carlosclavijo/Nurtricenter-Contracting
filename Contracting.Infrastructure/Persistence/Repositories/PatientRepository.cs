using Contracting.Domain.Patients;
using Contracting.Infrastructure.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence.Repositories;

public class PatientRepository(DomainDbContext DbContext) : IPatientRepository
{
	public async Task AddSync(Patient entity)
	{
		await DbContext.Patient.AddAsync(entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		var obj = await GetByIdAsync(id);
		DbContext.Patient.Remove(obj);
	}

	public async Task<Patient?> GetByIdAsync(Guid id, bool readOnly = false)
	{
		if (readOnly)
		{
			return await DbContext.Patient.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}
		else
		{
			return await DbContext.Patient.FindAsync(id);
		}
	}

	public Task UpdateAsync(Patient patient)
	{
		DbContext.Patient.Update(patient);
		return Task.CompletedTask;
	}
}
