using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Patients.CreatePatient;

public class CreatePatientHandler(IPatienteFactory PatientFactory, IPatientRepository PatientRepository, IUnitOfWork UnitOfWork) : IRequestHandler<CreatePatientCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = PatientFactory.Create(request.PatientName, request.PatientPhone);
        await PatientRepository.AddSync(patient);
        await UnitOfWork.CommitAsync(cancellationToken);
        return patient.Id;
    }
}