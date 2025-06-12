using System.Collections.Immutable;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.Persistence.DomainModel;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Persistence;

public class UnitOfWork(DomainDbContext DbContext, IPublisher Publisher) : IUnitOfWork, IOutboxDatabase<DomainEvent>
{
	private int _contractCount = 0;

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		_contractCount++;

		var domainEvents = DbContext.ChangeTracker
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
			await Publisher.Publish(e, cancellationToken);
		}

		if (_contractCount == 1)
		{
			await DbContext.SaveChangesAsync(cancellationToken);
		}
		else
		{
			_contractCount--;
		}
	}

	public DbSet<OutboxMessage<DomainEvent>> GetOutboxMessages()
	{
		return DbContext.OutboxMessages;
	}
}
