﻿using Contracting.Application.Contracts.CompleteContract;
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
	public async Task InProgressContractHandlerIsValid()
	{
		var contractId = Guid.NewGuid();
		var administratorId = Guid.NewGuid();
		var patientId = Guid.NewGuid();
		var startDate = DateTime.UtcNow.AddDays(1);
		var contract = new Contract(administratorId, patientId, ContractType.HalfMonth, startDate);

		_contractRepository
			.Setup(r => r.GetByIdAsync(It.Is<Guid>(id => id == contractId), It.IsAny<bool>()))
			.ReturnsAsync(contract);

		_unitOfWork
			.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
			.Returns(Task.CompletedTask);

		var handler = new InProgressContractHandler(_contractRepository.Object, _unitOfWork.Object);
		var command = new InProgressContractCommand(contractId);
		var result = await handler.Handle(command, CancellationToken.None);

		Assert.True(result);
		_contractRepository.Verify(r => r.GetByIdAsync(contractId, It.IsAny<bool>()), Times.Once);
		_unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
	}

	[Fact]
	public async Task InProgressContractHandlerIsInvalid()
	{
		var contractId = Guid.NewGuid();
		
		_contractRepository
			.Setup(r => r.GetByIdAsync(It.Is<Guid>(id => id == contractId), It.IsAny<bool>()))
			.ReturnsAsync((Contract?)null);

		var handler = new InProgressContractHandler(_contractRepository.Object, _unitOfWork.Object);
		var command = new InProgressContractCommand(contractId);
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));

		Assert.Equal("Contract not found", exception.Message);

		_unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
	}
}
