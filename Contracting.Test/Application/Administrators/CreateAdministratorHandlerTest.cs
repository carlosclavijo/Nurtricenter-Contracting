using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Administrators;
using Moq;

namespace Contracting.Test.Application.Administrators;

public class CreateAdministratorHandlerTest
{
    private readonly Mock<IAdministratorFactory> _administratorFactory;
    private readonly Mock<IAdministratorRepository> _administratorRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public CreateAdministratorHandlerTest()
    {
        _administratorFactory = new Mock<IAdministratorFactory>();
        _administratorRepository = new Mock<IAdministratorRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var name = "Carlos Clavijo";
        var phone = "77141516";

        var administrator = new Administrator(name, phone);
        var command = new CreateAdministratorCommand(name, phone);
        var cancellationToken = new CancellationTokenSource(1000);

        _administratorFactory
            .Setup(x => x.Create(command.AdministratorName, command.AdministratorPhone))
            .Returns(administrator);

        _administratorRepository
            .Setup(x => x.AddSync(It.IsAny<Administrator>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateAdministratorHandler(_administratorFactory.Object, _administratorRepository.Object, _unitOfWork.Object);
        var result = await handler.Handle(command, cancellationToken.Token);

        _administratorRepository.Verify(x => x.AddSync(It.IsAny<Administrator>()), Times.Once());
        _unitOfWork.Verify(x => x.CommitAsync(cancellationToken.Token), Times.Once());
    }
}
