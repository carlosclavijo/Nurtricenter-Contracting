using System;

namespace Contracting.Domain.Patients;

public class PatientFactory : IPatienteFactory
{
    public Patient Create(string patientName, string patientPhone)
    {
        if (string.IsNullOrWhiteSpace(patientName))
        {
            throw new ArgumentException("Patient name is required", nameof(patientName));
        }
        if (string.IsNullOrWhiteSpace(patientPhone))
        {
            throw new ArgumentException("Patient phone is required", nameof(patientPhone));
        }
        return new Patient(patientName, patientPhone);
    }
}