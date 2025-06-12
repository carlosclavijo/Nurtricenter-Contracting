using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Administrators.CreateAdministrator;

public record CreateAdministratorCommand(string AdministratorName, string AdministratorPhone) : IRequest<Result<Guid>>;
