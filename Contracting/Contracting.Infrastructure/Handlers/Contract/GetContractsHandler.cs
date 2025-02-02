using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Infrastructure.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Contract;

internal class GetContractsHandler : IRequestHandler<GetContractsQuery, IEnumerable<ContractDto>>
{
    private readonly StoredDbContext _dbContext;

    public GetContractsHandler(StoredDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ContractDto>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Contract.AsNoTracking()
            .Select(c => new ContractDto()
            {
                Id = c.Id,
                AdministratorId = c.AdministratorId,
                PatientId = c.PatientId,
                Type = c.TransactionType,
                Status = c.TransactionStatus,
                CreationDate = c.CreationDate,
                StartDate = c.StartDate,
                CompleteDate = c.CompletedDate ?? default,
                CostValue = c.TotalCost,
                DeliveryDays = c.DeliveryDays.Select(d => new DeliveryDayDto()
                {
                    Id = d.DeliveryDayId,
                    ContractId = d.ContractId,
                    DateTime = d.Date,
                    Street = d.Street,
                    Number = d.Number,
                    Longitude = d.Longitude,
                    Latitude = d.Latitude
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
