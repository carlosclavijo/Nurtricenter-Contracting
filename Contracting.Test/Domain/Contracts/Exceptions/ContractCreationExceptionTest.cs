using System;
using Contracting.Domain.Contracts.Exceptions;

namespace Contracting.Test.Domain.Contracts.Exceptions;

public class ContractCreationExceptionTest
{
    [Theory]
    [InlineData("Missing required fields")]
    [InlineData("Not enough information")]
    [InlineData("Please accept the terms and conditions before proceeding")]
    public void ContractCreationException_ShouldIncludeCustomMessage(string message)
    {
        var exception = new ContractCreationException(message);

        // Assert
        Assert.Equal("The contract cannot be created because: " + message, exception.Message);
    }
}
