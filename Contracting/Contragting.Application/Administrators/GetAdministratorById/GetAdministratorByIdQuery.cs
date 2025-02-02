using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Administrators.GetAdministrators;
using MediatR;

namespace Contracting.Application.Administrators.GetAdministratorById;

public record GetAdministratorByIdQuery(Guid AdministratorId) : IRequest<AdministratorDto>;
