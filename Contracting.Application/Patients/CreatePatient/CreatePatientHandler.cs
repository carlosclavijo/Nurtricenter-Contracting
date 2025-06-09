using System;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients;
using Joseco.DDD.Core.Results;
using MediatR;

namespace Contracting.Application.Patients.CreatePatient;

public class CreatePatientHandler(IPatienteFactory pacientFactory, IPatientRepository patientRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreatePatientCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = pacientFactory.Create(request.PatientName, request.PatientPhone);
        await patientRepository.AddSync(patient);
        await unitOfWork.CommitAsync(cancellationToken);
        return patient.Id;
    }
}