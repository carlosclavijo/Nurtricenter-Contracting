using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.UpdateDeliveryDayById;

public class UpdateDeliveryDayByIdHandler(IContractRepository contractRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateDeliveyDayByIdCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateDeliveyDayByIdCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetByIdAsync(request.ContractId);
        if (contract == null)
        {
            throw new InvalidOperationException("Contract not found");
        }

        var day = contract.DeliveryDays.FirstOrDefault(d => d.Id == request.DeliveryDayId);
        if (day == null)
        {
            throw new InvalidOperationException("Delivery day not found");
        }

        day.Update(request.Street, request.Number);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(contract.Id);
    }
}
