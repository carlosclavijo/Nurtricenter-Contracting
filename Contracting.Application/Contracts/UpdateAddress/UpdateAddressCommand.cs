using System;
using MediatR;

namespace Contracting.Application.Contracts.UpdateAddressById;

public record UpdateAddressCommand(Guid ContractId, DateTime FromDate, DateTime ToDate, string Street, int Number, double Longitude, double Latitude) : IRequest<bool>;