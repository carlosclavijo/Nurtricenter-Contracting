using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients;
using MediatR;

namespace Contracting.Application.Patients.CreatePatient;

internal class CreatePatientHandler : IRequestHandler<CreatePatientCommand, Guid>
{
    private readonly IPatienteFactory _patientFactory;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientHandler(IPatienteFactory patientFactory, IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientFactory = patientFactory;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = _patientFactory.Create(request.PatientName, request.PatientPhone);
        await _patientRepository.AddSync(patient);
        await _unitOfWork.CommitAsync(cancellationToken);
        return patient.Id;
    }
}