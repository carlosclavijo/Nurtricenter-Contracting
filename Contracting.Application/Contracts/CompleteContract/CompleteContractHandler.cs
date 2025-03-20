using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using MediatR;

namespace Contracting.Application.Contracts.CompleteContract;

public class CompleteContractHandler : IRequestHandler<CompleteContractCommand, bool>
{
    private readonly IContractRepository _contractRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteContractHandler(IContractRepository contractRepository, IUnitOfWork unitOfWork)
    {
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CompleteContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId);
        if (contract == null)
        {
            throw new ArgumentNullException("Contract is null", nameof(contract));
        }
        contract.Complete();
        contract.StartDate = contract.StartDate.ToUniversalTime();
        contract.CompletedDate = contract.CompletedDate?.ToUniversalTime();

        await _contractRepository.UpdateAsync(contract);

        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
