using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.Persistence;
using Contracting.Infrastructure.Persistence.DomainModel;
using Joseco.Outbox.Contracts.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Contracting.Test.Infrastructure.Persistence;

public class UnitOfWorkTests
{
	private readonly Mock<IPublisher> _publisher;
	private readonly UnitOfWork _unitOfWork;
	private readonly DbContextOptions<DomainDbContext> _options;
	private readonly TestDbContext _dbContext;

	public UnitOfWorkTests()
	{
		_options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
		_dbContext = new TestDbContext(_options);
		_publisher = new Mock<IPublisher>();
		_unitOfWork = new UnitOfWork(_dbContext, _publisher.Object);
	}

	[Fact]
	public async Task CommitAsyncValidTest()
	{
		var entity = new TestEntity();
		entity.AddDomainEvent(new DummyDomainEvent());

		_dbContext.Add(entity);

		var cancellationToken = new CancellationToken();
		await _unitOfWork.CommitAsync(cancellationToken);
		await _unitOfWork.CommitAsync(cancellationToken);

		_publisher.Verify(p => p.Publish(It.IsAny<DomainEvent>(), cancellationToken), Times.Once);
		Assert.False(_dbContext.ChangeTracker.HasChanges());
	}

	[Fact]
	public void GetOutboxMessagesValidTest()
	{
		var outbox = _unitOfWork.GetOutboxMessages();

		Assert.NotNull(outbox);
		Assert.IsAssignableFrom<DbSet<OutboxMessage<DomainEvent>>>(outbox);
	}

	public class TestEntity : Entity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
	}

    private record DummyDomainEvent : DomainEvent
    {
        public DummyDomainEvent() : base() { }
    }

	public class TestDbContext : DomainDbContext
	{
		public TestDbContext(DbContextOptions<DomainDbContext> options) : base(options) { }

		public DbSet<TestEntity> TestEntities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<TestEntity>();
		}
	}
}