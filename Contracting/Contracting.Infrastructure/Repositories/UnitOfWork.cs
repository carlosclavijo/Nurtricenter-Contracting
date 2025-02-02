using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Infrastructure.DomainModel;
using MediatR;

namespace Contracting.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly DomainDbContext _dbContext;
    private readonly IMediator _mediator;

    private int _contractCount = 0;

    public UnitOfWork(DomainDbContext dbContext)
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
}
