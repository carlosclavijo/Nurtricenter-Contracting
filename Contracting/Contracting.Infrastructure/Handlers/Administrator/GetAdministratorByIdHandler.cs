using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.Infrastructure.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Administrator;

internal class GetAdministratorByIdHandler : IRequestHandler<GetAdministratorByIdQuery, AdministratorDto>
{
    private readonly StoredDbContext _dbContext;

    public GetAdministratorByIdHandler(StoredDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdministratorDto> Handle(GetAdministratorByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.AdministratorId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(request.AdministratorId));
        }
        var administrator = await _dbContext.Administrator.AsNoTracking()
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
