using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using MediatR;

namespace Contracting.Application.Contracts.UpdateAddressById;

internal class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, bool>
{
    private readonly IContractFactory _contractFactory;
    private readonly IContractRepository _contractRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAddressHandler(IContractFactory contractFactory, IContractRepository contractRepository, IUnitOfWork unitOfWork)
    {
        _contractFactory = contractFactory;
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId);
        if (contract == null)
        {
            throw new ArgumentNullException("Contract is null", nameof(contract));
        }

        contract.UpdateAddresByDays(request.FromDate, request.ToDate, request.Street, request.Number, request.Longitude, request.Latitude);
        contract.StartDate = contract.StartDate.ToUniversalTime();
        contract.CompletedDate = contract.CompletedDate?.ToUniversalTime();

        await _contractRepository.UpdateAsync(contract);

        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
