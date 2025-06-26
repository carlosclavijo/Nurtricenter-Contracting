using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using MediatR;

namespace Contracting.Application.Contracts.InProgressContract;

public class InProgressContractHandler(IContractRepository contractRepository, IUnitOfWork UnitOfWork) : IRequestHandler<InProgressContractCommand, bool>
{
	public async Task<bool> Handle(InProgressContractCommand request, CancellationToken cancellationToken)
	{
		var contract = await contractRepository.GetByIdAsync(request.ContractId);
		if (contract == null)
		{
			throw new InvalidOperationException("Contract not found");
		}
		contract.InProgress();

		await UnitOfWork.CommitAsync(cancellationToken);

		return true;
	}
}
