using System;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.DomainModel;
using Contracting.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace Contracting.Test.Infrastructure.Repositories;

public class UnitOfWorkTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly UnitOfWork _unitOfWork;
    private readonly DbContextOptions<DomainDbContext> _options;
    private readonly DomainDbContext _dbContext;

    public UnitOfWorkTests()
    {
        _options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase("UnitOfWorkDb").Options;
        _dbContext = new DomainDbContext(_options);
        _mockMediator = new Mock<IMediator>();
        _unitOfWork = new UnitOfWork(_dbContext);
    }

    [Fact]
    public async Task CommitAsyncTest()
    {
        var cancellationToken = new CancellationToken();

        await _unitOfWork.CommitAsync(cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        Assert.False(_dbContext.ChangeTracker.HasChanges());

    }
}