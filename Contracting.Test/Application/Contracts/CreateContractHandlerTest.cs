using Contracting.Application.Contracts.CreateContract;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Moq;

namespace Contracting.Test.Application.Contracts;

public class CreateContractHandlerTest
{
    private readonly Mock<IContractFactory> _contractFactory;
    private readonly Mock<IContractRepository> _contractRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public CreateContractHandlerTest()
    {
        _contractFactory = new Mock<IContractFactory>();
        _contractRepository = new Mock<IContractRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Theory]
    [InlineData("FullMonth", ContractType.FullMonth)]
    [InlineData("HalfMonth", ContractType.HalfMonth)]
	public async Task HandleIsValid(string strType, ContractType type)
    {
        // Arrange
        Guid administratorId = Guid.NewGuid();
        Guid patientId = Guid.NewGuid();
        DateTime startDate = DateTime.Now.AddDays(2);
        string street = "Street";
        int number = 15;
        ICollection<CreateDeliveryDaysCommand> commands = [];

        var deliveryDaysCommand = new CreateDeliveryDaysCommand(startDate, street, number);
        commands.Add(deliveryDaysCommand);
        commands.Add(deliveryDaysCommand);
        commands.Add(deliveryDaysCommand);

        var command = new CreateContractCommand(administratorId, patientId, strType, startDate, commands);
        var cancellationToken = new CancellationTokenSource(1000).Token;
        var contract = new Contract(administratorId, patientId, type, startDate);

        if (type == ContractType.FullMonth)
        {
            _contractFactory.Setup(x => x.CreateFullMonthContract(administratorId, patientId, startDate)).Returns(contract);
        } 
        else
        {
            _contractFactory.Setup(x => x.CreateHalfMonthContract(administratorId, patientId, startDate)).Returns(contract);
        }
        _contractRepository.Setup(x => x.AddSync(It.IsAny<Contract>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.CommitAsync(cancellationToken)).Returns(Task.CompletedTask);

        
        var handler = new CreateContractHandler(_contractFactory.Object, _contractRepository.Object, _unitOfWork.Object);
        var result = await handler.Handle(command, cancellationToken);

        Assert.Equal(contract.Id, result.Value);
        if (type == ContractType.FullMonth)
        {
            _contractFactory.Verify(x => x.CreateFullMonthContract(administratorId, patientId, startDate), Times.Once);
        }
        else
        {
            _contractFactory.Verify(x => x.CreateHalfMonthContract(administratorId, patientId, startDate), Times.Once);
        }
        _contractRepository.Verify(x => x.AddSync(It.IsAny<Contract>()), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
    }
}
