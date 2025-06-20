using Contracting.Domain.Administrators;
using Contracting.Infrastructure.Persistence.DomainModel;
using Contracting.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Persistence.Repositories;

public class AdministratorRepositoryTest
{
	private readonly DbContextOptions<DomainDbContext> _options;
	private readonly DomainDbContext _dbContext;
	private readonly AdministratorRepository _repository;

	public AdministratorRepositoryTest()
	{
		_options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase(databaseName: "AdministratorRepositoryDb").Options;
		_dbContext = new DomainDbContext(_options);
		_repository = new AdministratorRepository(_dbContext);
	}

	[Fact]
	public async Task AddAsyncTest()
	{
		var name = "Carlos Clavijo";
		var phone = "66789789";

		var administrator = new Administrator(name, phone);

		await _repository.AddSync(administrator);
		await _dbContext.SaveChangesAsync();

		var result1 = await _repository.GetByIdAsync(administrator.Id);
		var result2 = await _repository.GetByIdAsync(administrator.Id, readOnly: true);

		Assert.NotNull(result1);
		Assert.Equal(administrator.Id, result1.Id);
		Assert.Equal(name, result1.Name);
		Assert.Equal(phone, result1.Phone);

		Assert.NotNull(result2);
		Assert.Equal(administrator.Id, result2.Id);
		Assert.Equal(name, result2.Name);
		Assert.Equal(phone, result2.Phone);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var name = "Carlos Clavijo";
		var phone = "66789789";

		var administrator = new Administrator(name, phone);

		await _repository.AddSync(administrator);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(administrator.Id);

		Assert.NotNull(result);

		await _repository.DeleteAsync(administrator.Id);
		await _dbContext.SaveChangesAsync();

		var nullResult = await _repository.GetByIdAsync(administrator.Id);
		Assert.Null(nullResult);
	}

	[Fact]
	public async Task UpdateAsync()
	{
		var name = "Carlos Clavijo";
		var phone = "66789789";

		var administrator = new Administrator(name, phone);

		await _repository.AddSync(administrator);
		await _dbContext.SaveChangesAsync();

		var result = await _repository.GetByIdAsync(administrator.Id);

		Assert.NotNull(result);
		Assert.Equal(administrator.Id, result.Id);
		Assert.Equal(name, result.Name);
		Assert.Equal(phone, result.Phone);

		var newName = "Alberto Fernandez";
		var newPhone = "77601415";

		administrator.Name = newName;
		administrator.Phone = newPhone;

		await _repository.UpdateAsync(administrator);
		await _dbContext.SaveChangesAsync();

		result = await _repository.GetByIdAsync(administrator.Id);

		Assert.NotNull(result);
		Assert.Equal(newName, result.Name);
		Assert.Equal(newPhone, result.Phone);
	}
}
