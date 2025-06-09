using System;
using Contracting.Domain.Patients;
using Contracting.Infrastructure.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
	private readonly DomainDbContext _dbContext;

	public PatientRepository(DomainDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddSync(Patient entity)
	{
		await _dbContext.Patient.AddAsync(entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		var obj = await GetByIdAsync(id);
		_dbContext.Patient.Remove(obj);
	}

	public async Task<Patient?> GetByIdAsync(Guid id, bool readOnly = false)
	{
		if (readOnly)
		{
			return await _dbContext.Patient.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}
		else
		{
			return await _dbContext.Patient.FindAsync(id);
		}
	}

	public Task UpdateAsync(Patient patient)
	{
		_dbContext.Patient.Update(patient);
		return Task.CompletedTask;
	}
}
