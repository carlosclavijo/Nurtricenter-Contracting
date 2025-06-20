using Contracting.Domain.Patients;
using Contracting.Infrastructure.Persistence.DomainModel;
using Contracting.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Persistence.Repositories;

public class PatientRepositoryTest
{
	private readonly DbContextOptions<DomainDbContext> _options;
	private readonly DomainDbContext _dbContext;
	private readonly PatientRepository _repository;

	public PatientRepositoryTest()
	{
		_options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase(databaseName: "PatientRepositoryDb").Options;
		_dbContext = new DomainDbContext(_options);
		_repository = new PatientRepository(_dbContext);
	}

	[Fact]
	public async Task AddAsyncTest()
	{
		var name = "Alberto Fernandez";
		var phone = "61231235";

		var patient = new Patient(name, phone);

		await _repository.AddSync(patient);
		await _dbContext.SaveChangesAsync();

		var result1 = await _repository.GetByIdAsync(patient.Id);
		var result2 = await _repository.GetByIdAsync(patient.Id, readOnly: true);

		Assert.NotNull(result1);
		Assert.Equal(patient.Id, result1.Id);
		Assert.Equal(name, result1.Name);
		Assert.Equal(phone, result1.Phone);

		Assert.NotNull(result2);
		Assert.Equal(patient.Id, result2.Id);
		Assert.Equal(name, result2.Name);
		Assert.Equal(phone, result2.Phone);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var name = "Alberto Fernandez";
		var phone = "61231235";

		var patient = new Patient(name, phone);

		await _repository.AddSync(patient);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(patient.Id);

		Assert.NotNull(result);

		await _repository.DeleteAsync(patient.Id);
		await _dbContext.SaveChangesAsync();

		var nullResult = await _repository.GetByIdAsync(patient.Id);
		Assert.Null(nullResult);
	}

	[Fact]
	public async Task UpdateAsync()
	{
		var name = "Alberto Fernandez";
		var phone = "61231235";

		var patient = new Patient(name, phone);

		await _repository.AddSync(patient);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(patient.Id);

		Assert.NotNull(result);
		Assert.Equal(patient.Id, result.Id);
		Assert.Equal(name, result.Name);
		Assert.Equal(phone, result.Phone);

		var newName = "Carlos Clavijo";
		var newPhone = "79083421";

		patient.Name = newName;
		patient.Phone = newPhone;

		await _repository.UpdateAsync(patient);
		await _dbContext.SaveChangesAsync();

		result = await _repository.GetByIdAsync(patient.Id);

		Assert.NotNull(result);
		Assert.Equal(newName, result.Name);
		Assert.Equal(newPhone, result.Phone);
	}
}
