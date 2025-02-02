using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Application.Administrators.GetAdministrators;

public class AdministratorDto
{
    public Guid Id { get; set; }
    public string AdministratorName { get; set; }
    public string AdministratorPhone { get; set; }
}
