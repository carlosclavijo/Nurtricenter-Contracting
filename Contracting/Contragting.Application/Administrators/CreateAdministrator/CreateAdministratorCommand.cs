using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contracting.Application.Administrators.CreateAdministrator;

public record CreateAdministratorCommand(string AdministratorName, string AdministratorPhone) : IRequest<Guid>;
