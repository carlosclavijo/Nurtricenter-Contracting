using System;
using Contracting.Application.Contracts.UpdateAddressById;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Infrastructure.DomainModel;
using Contracting.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class UpdateAddressHandlerTest
{
    private readonly DbContextOptions<DomainDbContext> _options;
    private readonly DomainDbContext _dbContext;
    private readonly ContractRepository _contractRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public UpdateAddressHandlerTest()
    {
        _options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase(databaseName: "UpdateAddressHandlerDb").Options;

        _dbContext = new DomainDbContext(_options);
        _contractRepository = new ContractRepository(_dbContext);
        _unitOfWork = new Mock<IUnitOfWork>();
    }

    public async Task HandleIsValid()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        var administratorId = Guid.NewGuid();
        var patientId = Guid.NewGuid();
        var fromDate = DateTime.UtcNow.AddDays(1);
        var toDate = DateTime.UtcNow.AddDays(5);
        var street = "New Street";
        var number = 20;
        var longitude = 2.1234;
        var latitude = -4.9876;

        var contract = new Contract(administratorId, patientId, ContractType.FullMonth, DateTime.UtcNow);
        _dbContext.Add(contract);
        await _dbContext.SaveChangesAsync();

        var command = new UpdateAddressCommand(contractId, fromDate, toDate, street, number, longitude, latitude);
        var cancellationToken = new CancellationTokenSource(1000).Token;

        _unitOfWork.Setup(x => x.CommitAsync(cancellationToken)).Returns(Task.CompletedTask);

        var handler = new UpdateAddressHandler(Mock.Of<IContractFactory>(), _contractRepository, _unitOfWork.Object);
        var result = await handler.Handle(command, cancellationToken);

        Assert.True(result);

        var updatedContract = await _contractRepository.GetByIdAsync(contractId);
        Assert.NotNull(updatedContract);
        _unitOfWork.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
    }
}
