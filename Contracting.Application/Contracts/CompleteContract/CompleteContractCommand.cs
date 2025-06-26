using MediatR;

namespace Contracting.Application.Contracts.CompleteContract;

public record CompleteContractCommand(Guid ContractId) : IRequest<bool>;
