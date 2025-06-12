using Contracting.Application.Patients.GetPatients;
using Contracting.Infrastructure.Persistence.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Patients;

public class GetPatientsHandler(StoredDbContext DbContext) : IRequestHandler<GetPatientsQuery, IEnumerable<PatientDto>>
{
	public async Task<IEnumerable<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        return await DbContext.Patient.AsNoTracking()
            .Select(i => new  PatientDto()
            {
                Id = i.Id,
                PatientName = i.PatientName,
                PatientPhone = i.PatientPhone
            })
            .ToListAsync(cancellationToken);
    }
}
