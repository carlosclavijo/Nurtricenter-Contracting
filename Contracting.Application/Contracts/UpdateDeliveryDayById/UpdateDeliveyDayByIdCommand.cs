using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.UpdateDeliveryDayById;

public record UpdateDeliveyDayByIdCommand(Guid ContractId, Guid DeliveryDayId, string Street, int Number): IRequest<Result<Guid>>;
