using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Contracts.GetContractById;
using MediatR;

namespace Contracting.Application.Contracts.GetContracts;

public record GetContractsQuery(string SearchTerm) : IRequest<IEnumerable<ContractDto>>;
