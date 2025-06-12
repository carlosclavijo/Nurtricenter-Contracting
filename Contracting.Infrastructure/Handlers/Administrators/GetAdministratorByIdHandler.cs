using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Infrastructure.Persistence.StoredModel;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Administrators;

public class GetAdministratorByIdHandler(StoredDbContext DbContext) : IRequestHandler<GetAdministratorByIdQuery, Result<AdministratorDto>>
{
    public async Task<Result<AdministratorDto>> Handle(GetAdministratorByIdQuery request, CancellationToken cancellationToken)
    {
        var administrator = await DbContext.Administrator.AsNoTracking()
            .Where(t => t.Id == request.AdministratorId)
            .Select(t => new AdministratorDto()
            {
                Id = t.Id,
                AdministratorName = t.AdministratorName,
                AdministratorPhone = t.AdministratorPhone
            })
            .FirstOrDefaultAsync(cancellationToken);
        return administrator;
    }
}
