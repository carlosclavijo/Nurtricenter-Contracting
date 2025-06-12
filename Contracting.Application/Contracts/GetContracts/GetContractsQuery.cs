using Contracting.Application.Contracts.GetContractById;
using MediatR;

namespace Contracting.Application.Contracts.GetContracts;

public record GetContractsQuery(string SearchTerm) : IRequest<IEnumerable<ContractDto>>;
