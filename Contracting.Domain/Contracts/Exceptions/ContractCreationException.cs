namespace Contracting.Domain.Contracts.Exceptions;

public class ContractCreationException(string message) : Exception("The contract cannot be created because: " + message);
