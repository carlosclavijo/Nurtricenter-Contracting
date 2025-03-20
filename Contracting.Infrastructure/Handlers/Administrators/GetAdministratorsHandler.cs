using System;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Infrastructure.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Administrators;

public class GetAdministratorsHandler : IRequestHandler<GetAdministratorsQuery, IEnumerable<AdministratorDto>>
{
    private readonly StoredDbContext _dbContext;

    public GetAdministratorsHandler(StoredDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AdministratorDto>> Handle(GetAdministratorsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Administrator.AsNoTracking()
            .Select(i => new AdministratorDto()
            {
                Id = i.Id,
                AdministratorName = i.AdministratorName,
                AdministratorPhone = i.AdministratorPhone
            })
            .ToListAsync(cancellationToken);
    }
}
