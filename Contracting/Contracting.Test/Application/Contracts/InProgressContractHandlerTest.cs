using System;
using Contracting.Application.Contracts.CompleteContract;
using Contracting.Application.Contracts.InProgressContract;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class InProgressContractHandlerTest
{
    private readonly Mock<IContractRepository> _contractRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public InProgressContractHandlerTest()
    {
        _contractRepository = new Mock<IContractRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async void HandleIsValid()
    {
        Contract contract = new Contract(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now.AddDays(2));
        InProgressContractCommand command = new InProgressContractCommand(contract.Id);
        var cancellationToken = new CancellationTokenSource(1000);

        _contractRepository.Setup(x => x.GetByIdAsync(contract.Id, false)).ReturnsAsync(contract);

        InProgressContractHandler handler = new InProgressContractHandler(_contractRepository.Object, _unitOfWork.Object);
        var result = await handler.Handle(command, cancellationToken.Token);

        _contractRepository.Verify(x => x.UpdateAsync(contract), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(cancellationToken.Token), Times.Once);
    }

    [Fact]
    public async void HandleIsInValid()
    {
        Guid emptyId = Guid.Empty;
        InProgressContractCommand command = new InProgressContractCommand(emptyId);
        var cancellationToken = new CancellationTokenSource(1000);

        _contractRepository.Setup(x => x.GetByIdAsync(emptyId, false));

        InProgressContractHandler handler = new InProgressContractHandler(_contractRepository.Object, _unitOfWork.Object);

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(command, cancellationToken.Token));
        Assert.Equal("contract (Parameter 'Contract is null')", exception.Message);
    }
}
