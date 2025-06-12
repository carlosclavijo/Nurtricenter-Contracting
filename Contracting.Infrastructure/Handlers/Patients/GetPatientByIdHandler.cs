using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using Contracting.Infrastructure.Persistence.StoredModel;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Patients;

public class GetPatientByIdHandler(StoredDbContext DbContext) : IRequestHandler<GetPatientByIdQuery, Result<PatientDto>>
{
	public async Task<Result<PatientDto>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await DbContext.Patient.AsNoTracking()
            .Where(p => p.Id  == request.PatientId)
            .Select(p => new PatientDto()
            {
                Id = p.Id,
                PatientName = p.PatientName,
                PatientPhone = p.PatientPhone
            })
            .FirstOrDefaultAsync(cancellationToken);
        return patient;
    }
}
