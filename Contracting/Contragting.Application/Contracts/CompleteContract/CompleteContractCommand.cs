using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contracting.Application.Contracts.CompleteContract;

public record CompleteContractCommand(Guid ContractId) : IRequest<bool>;