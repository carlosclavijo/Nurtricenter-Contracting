using Contracting.Application.Contracts.GetContracts;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.GetDeliveryDay;

public record GetDeliveryDayQuery(Guid DeliveryDayId) : IRequest<Result<DeliveryDayDto>>;