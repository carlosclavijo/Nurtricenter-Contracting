using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Administrators;

public interface IAdministratorFactory
{
    Administrator Create(string administratorName, string administratorPhone);
}