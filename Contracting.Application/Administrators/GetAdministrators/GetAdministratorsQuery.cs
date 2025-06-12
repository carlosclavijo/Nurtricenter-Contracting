using MediatR;

namespace Contracting.Application.Administrators.GetAdministrators;

public record GetAdministratorsQuery(string SearchTerm) : IRequest<IEnumerable<AdministratorDto>>;
