using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using MediatR;

namespace Contracting.Application.Contracts.UpdateAddressById;

public class UpdateAddressHandler(IContractRepository ContractRepository, IUnitOfWork UnitOfWork) : IRequestHandler<UpdateAddressCommand, bool>
{
	public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var contract = await ContractRepository.GetByIdAsync(request.ContractId);

        contract.UpdateAddressByDays(request.FromDate, request.ToDate, request.Street, request.Number, request.Longitude, request.Latitude);
        contract.StartDate = contract.StartDate.Kind == DateTimeKind.Unspecified
                     ? contract.StartDate.ToUniversalTime()
                     : contract.StartDate;
        contract.CompletedDate = contract.CompletedDate?.Kind == DateTimeKind.Unspecified
                                 ? contract.CompletedDate?.ToUniversalTime()
                                 : contract.CompletedDate;

        await ContractRepository.UpdateAsync(contract);
        await UnitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
