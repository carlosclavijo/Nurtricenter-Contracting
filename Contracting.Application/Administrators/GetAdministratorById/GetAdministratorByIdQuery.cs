using Contracting.Application.Administrators.GetAdministrators;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Administrators.GetAdministratorById;

public record GetAdministratorByIdQuery(Guid AdministratorId): IRequest<Result<AdministratorDto>>;
