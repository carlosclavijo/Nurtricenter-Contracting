using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contracting.Application.Contracts.InProgressContract;

public record InProgressContractCommand(Guid ContractId) : IRequest<bool>;