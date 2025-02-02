using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Contracts.Exceptions;
using MediatR;

namespace Contracting.Application.Contracts.InProgressContract;

internal class InProgressContractHandler : IRequestHandler<InProgressContractCommand, bool>
{
    private readonly IContractFactory _contractFactory;
    private readonly IContractRepository _contractRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InProgressContractHandler(IContractRepository contractRepository, IUnitOfWork unitOfWork)
    {
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(InProgressContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId);
        if (contract == null)
        {
            throw new ArgumentNullException("Contract is null", nameof(contract));
        }
        contract.InProgress();
        contract.StartDate = contract.StartDate.ToUniversalTime();
        contract.CompletedDate = contract.CompletedDate?.ToUniversalTime();

        await _contractRepository.UpdateAsync(contract);

        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
