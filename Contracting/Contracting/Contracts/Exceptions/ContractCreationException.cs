using System;

namespace Contracting.Domain.Contracts.Exceptions;

public class ContractCreationException : Exception
{
    public ContractCreationException(string message) : base("The contract cannot be created because: " + message)
    { 
    }
}
