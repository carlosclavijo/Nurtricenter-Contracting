using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using MediatR;

namespace Contracting.Application.Contracts.UpdateAddressById;

public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, bool>
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

        contract.UpdateAddressByDays(request.FromDate, request.ToDate, request.Street, request.Number, request.Longitude, request.Latitude);
        contract.StartDate = contract.StartDate.Kind == DateTimeKind.Unspecified
                     ? contract.StartDate.ToUniversalTime()
                     : contract.StartDate;
        contract.CompletedDate = contract.CompletedDate?.Kind == DateTimeKind.Unspecified
                                 ? contract.CompletedDate?.ToUniversalTime()
                                 : contract.CompletedDate;

        await _contractRepository.UpdateAsync(contract);
        await _unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
