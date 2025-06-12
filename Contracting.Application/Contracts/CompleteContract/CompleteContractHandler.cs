using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.CompleteContract;

public class CompleteContractHandler(IContractRepository ContractRepository, IUnitOfWork UnitOfWork) : IRequestHandler<CompleteContractCommand, bool>
{
    public async Task<bool> Handle(CompleteContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await ContractRepository.GetByIdAsync(request.ContractId);

        contract.Complete();
        contract.StartDate = contract.StartDate.ToUniversalTime();
        contract.CompletedDate = contract.CompletedDate?.ToUniversalTime();

        await ContractRepository.UpdateAsync(contract);
        await UnitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
