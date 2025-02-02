using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using Contracting.Infrastructure.StoredModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Infrastructure.Handlers.Patient;

internal class GetPatientByIdHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
{
    private readonly StoredDbContext _dbContext;

    public GetPatientByIdHandler(StoredDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.PatientId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(request.PatientId));
        }

        var patient = await _dbContext.Patient.AsNoTracking()
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
