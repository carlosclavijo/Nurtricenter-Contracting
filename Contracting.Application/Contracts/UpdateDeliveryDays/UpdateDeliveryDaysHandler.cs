using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.UpdateDeliveryDays;

public class UpdateDeliveryDaysHandler(IContractRepository contractRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateDeliveryDaysCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateDeliveryDaysCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetByIdAsync(request.ContractId);
		if (contract == null)
		{
			throw new InvalidOperationException("Contract not found");
		}

		var daysToUpdate = contract.DeliveryDays
            .Where(d => d.Date.Date >= request.FirstDate.Date && d.Date.Date <= request.LastDate.Date)
            .ToList();

        if (daysToUpdate.Count == 0)
        {
            throw new InvalidOperationException("No delivery days found in the specified range.");
		}

        foreach (var day in daysToUpdate)
        {
            day.Update(request.Street, request.Number);
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(contract.Id);
    }
}
