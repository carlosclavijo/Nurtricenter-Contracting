using System;
using MediatR;

namespace Contracting.Application.Contracts.InProgressContract;

public record InProgressContractCommand(Guid ContractId) : IRequest<bool>;