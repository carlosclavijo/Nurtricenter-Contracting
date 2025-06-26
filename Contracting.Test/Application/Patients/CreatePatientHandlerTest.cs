using Contracting.Application.Patients.CreatePatient;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients;
using Moq;

namespace Contracting.Test.Application.Patients;

public class CreatePatientHandlerTest
{
    private readonly Mock<IPatienteFactory> _patientFactory;
    private readonly Mock<IPatientRepository> _patientRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public CreatePatientHandlerTest()
    {
        _patientFactory = new Mock<IPatienteFactory>();
        _patientRepository = new Mock<IPatientRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var name = "Alberto Fernandez";
        var phone = "78645975";

        var patient = new Patient(name, phone);
        var command = new CreatePatientCommand(name, phone);
        var cancellationToken = new CancellationTokenSource(1000);

        _patientFactory
            .Setup(x => x.Create(command.PatientName, command.PatientPhone))
            .Returns(patient);

        _patientRepository
            .Setup(x => x.AddSync(It.IsAny<Patient>()))
            .Returns(Task.CompletedTask);

        var handler = new CreatePatientHandler(_patientFactory.Object, _patientRepository.Object, _unitOfWork.Object);
        await handler.Handle(command, cancellationToken.Token);

        _patientRepository.Verify(x => x.AddSync(It.IsAny<Patient>()), Times.Once());
        _unitOfWork.Verify(x => x.CommitAsync(cancellationToken.Token), Times.Once());
    }
}
