using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Infrastructure.Persistence.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Contracts;

public class GetContractsHandler(StoredDbContext DbContext) : IRequestHandler<GetContractsQuery, IEnumerable<ContractDto>>
{
	public async Task<IEnumerable<ContractDto>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
    {
        return await DbContext.Contract.AsNoTracking()
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
