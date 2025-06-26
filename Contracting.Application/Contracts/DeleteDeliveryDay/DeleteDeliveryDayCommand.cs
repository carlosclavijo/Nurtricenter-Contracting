using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.DeleteDeliveryDay;

public record DeleteDeliveryDayCommand(Guid ContractId, Guid DeliveryDayId) : IRequest<Result<Guid>>;
