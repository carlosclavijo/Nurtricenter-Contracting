using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.InProgressContract;

public class InProgressContractHandler(IContractRepository ContractRepository, IUnitOfWork UnitOfWork) : IRequestHandler<InProgressContractCommand, bool>
{
    public async Task<bool> Handle(InProgressContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await ContractRepository.GetByIdAsync(request.ContractId);
        contract.InProgress();
        contract.StartDate = contract.StartDate.ToUniversalTime();
        contract.CompletedDate = contract.CompletedDate?.ToUniversalTime();

        await ContractRepository.UpdateAsync(contract);
        await UnitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
