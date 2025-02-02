using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Shared;
using MediatR;

namespace Contracting.Application.Contracts.CreateContract;

public record CreateContractCommand(Guid AdministratorId, Guid PatientId, string Type, DateTime StartDate, ICollection<CreateDeliveryDaysCommand> Days) : IRequest<Guid>;

public record CreateDeliveryDaysCommand(DateTime Start, DateTime Ends, string Street, int Number, double Longitude, double Latitude);
