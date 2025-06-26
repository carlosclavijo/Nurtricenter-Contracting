using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using MediatR;

namespace Contracting.Application.Contracts.CompleteContract;

public class CompleteContractHandler(IContractRepository ContractRepository, IUnitOfWork UnitOfWork) : IRequestHandler<CompleteContractCommand, bool>
{
	public async Task<bool> Handle(CompleteContractCommand request, CancellationToken cancellationToken)
	{
		var contract = await ContractRepository.GetByIdAsync(request.ContractId);
		if (contract == null)
		{
			throw new InvalidOperationException("Contract not found");
		}
		contract.Complete();

		await UnitOfWork.CommitAsync(cancellationToken);

		return true;
	}
}
