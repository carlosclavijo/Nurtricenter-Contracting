using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Domain.Contracts.Exceptions;

public class ContractCreationException : Exception
{
    public ContractCreationException(string message)
        : base("The contract cannot be created because: " + message)
    { 
    }
}
