using Contracting.Domain.Contracts;
using Contracting.Infrastructure.Persistence.DomainModel;
using Contracting.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Persistence.Repositories;

public class ContractRespositoryTest
{
	private readonly DbContextOptions<DomainDbContext> _options;
	private readonly DomainDbContext _dbContext;
	private readonly ContractRepository _repository;

	public ContractRespositoryTest()
	{
		_options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase(databaseName: "PatientRepositoryDb").Options;
		_dbContext = new DomainDbContext(_options);
		_repository = new ContractRepository(_dbContext);
	}

	[Fact]
	public async Task AddAsyncTest()
	{
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var type = ContractType.FullMonth;
		var date = DateTime.UtcNow;

		var contract = new Contract(administratorId, patientId, type, date);

		await _repository.AddSync(contract);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(contract.Id);

		Assert.NotNull(result);
		Assert.Equal(contract.Id, result.Id);
		Assert.Equal(contract.AdministratorId, result.AdministratorId);
		Assert.Equal(contract.PatientId, result.PatientId);
		Assert.Equal(type, result.Type);
		Assert.Equal(date, result.StartDate);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var type = ContractType.FullMonth;
		var date = DateTime.UtcNow;

		var contract = new Contract(administratorId, patientId, type, date);

		await _repository.AddSync(contract);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(contract.Id);

		Assert.NotNull(result);

		await _repository.DeleteAsync(contract.Id);
		await _dbContext.SaveChangesAsync();

		var nullResult = await _repository.GetByIdAsync(contract.Id);
		Assert.Null(nullResult);
	}

	[Fact]
	public async Task UpdateAsync()
	{
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var type = ContractType.FullMonth;
		var date = DateTime.UtcNow;

		var contract = new Contract(administratorId, patientId, type, date);

		await _repository.AddSync(contract);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(contract.Id);

		Assert.NotNull(result);
		Assert.Equal(contract.Id, result.Id);
		Assert.Equal(contract.AdministratorId, result.AdministratorId);
		Assert.Equal(contract.PatientId, result.PatientId);
		Assert.Equal(type, result.Type);
		Assert.Equal(date, result.StartDate);

		var newAdministratorId = Guid.NewGuid();
		var newPatientId = Guid.NewGuid();
		var newType = ContractType.HalfMonth;
		var newDate = DateTime.UtcNow.AddDays(1);

		contract.AdministratorId = newAdministratorId;
		contract.PatientId = newPatientId;
		contract.Type = newType;
		contract.StartDate = newDate;

		await _repository.UpdateAsync(contract);
		await _dbContext.SaveChangesAsync();

		result = await _repository.GetByIdAsync(contract.Id);

		Assert.NotNull(result);
		Assert.NotNull(result);
		Assert.Equal(contract.Id, result.Id);
		Assert.Equal(newAdministratorId, result.AdministratorId);
		Assert.Equal(newPatientId, result.PatientId);
		Assert.Equal(newType, result.Type);
		Assert.Equal(newDate, result.StartDate);
	}
}
