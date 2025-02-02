using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contracting.Application.Administrators.GetAdministrators;

public record GetAdministratorsQuery(string SearchTerm) : IRequest<IEnumerable<AdministratorDto>>;
