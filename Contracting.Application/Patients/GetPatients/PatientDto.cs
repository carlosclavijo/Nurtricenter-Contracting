using System;

namespace Contracting.Application.Patients.GetPatients;

public class PatientDto
{
    public Guid Id { get; set; }
    public string PatientName { get; set; }
    public string PatientPhone { get; set; }
}
