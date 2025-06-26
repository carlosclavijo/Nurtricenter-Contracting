using Contracting.Application.Contracts.GetContracts;
using Contracting.Application.Contracts.GetDeliveryDay;
using Contracting.Infrastructure.Persistence.StoredModel;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Contracts;

public class GetDeliveryDayHandler(StoredDbContext DbContext) : IRequestHandler<GetDeliveryDayQuery, Result<DeliveryDayDto>>
{
	public async Task<Result<DeliveryDayDto>> Handle(GetDeliveryDayQuery request, CancellationToken cancellationToken)
	{
		var delivery = await DbContext.DeliveryDay.AsNoTracking()
			   .Where(d => d.Id == request.DeliveryDayId)
			   .Select(c => new DeliveryDayDto()
			   {
				   Id = c.Id,
				   ContractId = c.ContractId,
				   DateTime = c.Date,
				   Street = c.Street,
				   Number = c.Number
			   })
			   .FirstOrDefaultAsync(cancellationToken);
		return delivery;
	}
}
