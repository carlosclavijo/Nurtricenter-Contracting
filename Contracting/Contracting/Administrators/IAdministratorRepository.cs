using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Administrators;

public interface IAdministratorRepository : IRepository<Administrator>
{
    Task UpdateAsync(Administrator administrador);
    Task DeleteAsync(Guid id);
}
