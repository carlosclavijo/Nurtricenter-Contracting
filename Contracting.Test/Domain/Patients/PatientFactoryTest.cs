using Contracting.Domain.Patients;

namespace Contracting.Test.Domain.Patients;

public class PatientFactoryTest
{
    private readonly PatientFactory _factory = new();

    [Fact]
    public void CreateValidPatientFactory()
    {
        string name = "Alberto Fernandez";
        string phone = "77141516";

        var patient = _factory.Create(name, phone);

        Assert.NotNull(patient);
        Assert.Equal(patient.Name.NormalizeName(name), patient.Name);
        Assert.Equal(phone, patient.Phone);
    }

    [Fact]
    public void CreateInvalidNamePatientFactory()
    {
        string name = "";
        string phone = "71234567";

        var exception = Assert.Throws<ArgumentException>(() => _factory.Create(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Patient name is required (Parameter 'patientName')", exception.Message);
    }

    [Fact]
    public void CreateInvalidPhonePatientFactory()
    {
        string name = "Alberto Fernandez";
        string phone = "";

        var exception = Assert.Throws<ArgumentException>(() => _factory.Create(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Patient phone is required (Parameter 'patientPhone')", exception.Message);
    }

}
