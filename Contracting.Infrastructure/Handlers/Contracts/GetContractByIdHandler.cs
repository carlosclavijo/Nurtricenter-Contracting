using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Infrastructure.Persistence.StoredModel;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Contracts;

public class GetContractByIdHandler(StoredDbContext DbContext) : IRequestHandler<GetContractByIdQuery, Result<ContractDto>>
{
	public async Task<Result<ContractDto>> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
    {
        var contract = await DbContext.Contract.AsNoTracking()
               .Where(c => c.Id == request.ContractId)
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
                       Id = d.Id,
                       ContractId = d.ContractId,
                       DateTime = d.Date,
                       Street = d.Street,
                       Number = d.Number
                   }).ToList()
               })
               .FirstOrDefaultAsync(cancellationToken);
        return contract;
    }
}
