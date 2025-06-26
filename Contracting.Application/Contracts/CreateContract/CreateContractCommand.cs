using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Contracts.CreateContract;

public record CreateContractCommand(Guid AdministratorId, Guid PatientId, string Type, DateTime StartDate, ICollection<CreateDeliveryDaysCommand> Days) : IRequest<Result<Guid>>;

public record CreateDeliveryDaysCommand(DateTime Start, string Street, int Number);
