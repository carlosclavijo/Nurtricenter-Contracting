using System;

namespace Contracting.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    Task<TEntity?> GetByIdAsync(Guid id, bool readOnly = false);
    Task AddSync(TEntity entity);
}
