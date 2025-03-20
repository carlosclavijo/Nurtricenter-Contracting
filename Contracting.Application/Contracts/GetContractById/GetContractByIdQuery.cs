using System;
using MediatR;

namespace Contracting.Application.Contracts.GetContractById;

public record GetContractByIdQuery(Guid ContractId) : IRequest<ContractDto>;
