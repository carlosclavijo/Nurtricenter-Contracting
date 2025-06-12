namespace Contracting.Domain.Patients;

public interface IPatienteFactory
{
    Patient Create(string patientName, string patientPhone);
}

