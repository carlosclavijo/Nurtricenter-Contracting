using System;
using MediatR;

namespace Contracting.Application.Administrators.CreateAdministrator;

public record CreateAdministratorCommand(string AdministratorName, string AdministratorPhone) : IRequest<Guid>;
