using Contracting.Application.Contracts.GetContractById;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.UpdateDeliveryDays;

public record UpdateDeliveryDaysCommand(Guid ContractId, DateTime FirstDate, DateTime LastDate, string Street, int Number) : IRequest<Result<Guid>>;
