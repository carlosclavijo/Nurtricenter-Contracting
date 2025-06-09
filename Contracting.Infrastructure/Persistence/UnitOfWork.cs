using System.Collections.Immutable;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.Persistence.DomainModel;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork, IOutboxDatabase<DomainEvent>
{
	private readonly DomainDbContext _dbContext;
	private readonly IMediator _mediator;

	private int _contractCount = 0;

	public UnitOfWork(DomainDbContext dbContext, IMediator mediator)
	{
		_dbContext = dbContext;
	}

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		_contractCount++;

		var domainEvents = _dbContext.ChangeTracker
			.Entries<Entity>()
			.Where(x => x.Entity.DomainEvents.Any())
			.Select(x =>
			{
				var domainEvents = x.Entity
					.DomainEvents
					.ToImmutableArray();
				x.Entity.ClearDomainEvents();
				return domainEvents;
			})
			.SelectMany(domainEvents => domainEvents)
			.ToList();

		foreach (var e in domainEvents)
		{
			await _mediator.Publish(e, cancellationToken);
		}

		if (_contractCount == 1)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
		else
		{
			_contractCount--;
		}
	}

	public DbSet<OutboxMessage<DomainEvent>> GetOutboxMessages()
	{
		return _dbContext.OutboxMessages;
	}
}
