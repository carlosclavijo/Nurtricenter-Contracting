using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Infrastructure.Persistence.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Administrators;

public class GetAdministratorsHandler(StoredDbContext DbContext) : IRequestHandler<GetAdministratorsQuery, IEnumerable<AdministratorDto>>
{
    public async Task<IEnumerable<AdministratorDto>> Handle(GetAdministratorsQuery request, CancellationToken cancellationToken)
    {
        return await DbContext.Administrator.AsNoTracking()
            .Select(i => new AdministratorDto()
            {
                Id = i.Id,
                AdministratorName = i.AdministratorName,
                AdministratorPhone = i.AdministratorPhone
            })
            .ToListAsync(cancellationToken);
    }
}
