using System;
using Contracting.Application.Patients.GetPatients;
using Contracting.Infrastructure.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Patients;

public class GetPatientsHandler : IRequestHandler<GetPatientsQuery, IEnumerable<PatientDto>>
{
    private readonly StoredDbContext _dbContext;

    public GetPatientsHandler(StoredDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Patient.AsNoTracking()
            .Select(i => new  PatientDto()
            {
                Id = i.Id,
                PatientName = i.PatientName,
                PatientPhone = i.PatientPhone
            })
            .ToListAsync(cancellationToken);
    }
}
