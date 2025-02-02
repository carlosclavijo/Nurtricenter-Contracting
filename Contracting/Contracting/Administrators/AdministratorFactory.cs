using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Administrators;

public class AdministradorFactory : IAdministratorFactory
{
    public Administrator Create(string administratorName, string administratorPhone)
    {
        if (string.IsNullOrWhiteSpace(administratorName))
        {
            throw new ArgumentException("Administrator name is required", nameof(administratorName));
        }
        if (string.IsNullOrWhiteSpace(administratorPhone))
        {
            throw new ArgumentException("Administrator phone is required", nameof(administratorPhone));
        }
        return new Administrator(administratorName, administratorPhone);
    }
}