using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Administrators;

public interface IAdministratorRepository : IRepository<Administrator>
{
    Task UpdateAsync(Administrator administrador);
    Task DeleteAsync(Guid id);
}
