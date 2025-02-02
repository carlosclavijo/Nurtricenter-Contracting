using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Contracts.GetContractById;
using MediatR;

namespace Contracting.Application.Contracts.UpdateAddressById;

public record UpdateAddressCommand(Guid ContractId, DateTime FromDate, DateTime ToDate, string Street, int Number, double Longitude, double Latitude) : IRequest<bool>;