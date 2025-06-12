using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.GetContractById;

public record GetContractByIdQuery(Guid ContractId) : IRequest<Result<ContractDto>>;
