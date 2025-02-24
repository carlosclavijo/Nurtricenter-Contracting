using System;
using Contracting.Application.Administrators.GetAdministrators;
using MediatR;

namespace Contracting.Application.Administrators.GetAdministratorById;

public record GetAdministratorByIdQuery(Guid AdministratorId) : IRequest<AdministratorDto>;
