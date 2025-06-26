using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.DeleteDeliveryDay;

public class DeleteDeliveryDayHandler(IContractRepository contractRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteDeliveryDayCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteDeliveryDayCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetByIdAsync(request.ContractId);
        if (contract == null)
        {
			throw new InvalidOperationException("Contract not found");
		}

        var dayToRemove = contract.DeliveryDays.FirstOrDefault(d => d.Id == request.DeliveryDayId);
        if (dayToRemove == null)
        {
			throw new InvalidOperationException("DeliveryDay not found");
		}

		dayToRemove.Delete();
        contract.DeliveryDays.Remove(dayToRemove);

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success(request.ContractId);
    }
}
