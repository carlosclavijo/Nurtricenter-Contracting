using Contracting.Domain.Administrators;
using Contracting.Domain.Patients;

namespace Contracting.Test.Domain.Patients;

public class PatientTest
{
    [Theory]
    [InlineData("alberto fernandez", "78945612")]
    [InlineData("Alberto Fernandez", "75386421")]
    [InlineData("Alberto    Fernandez", "77141516")]
    [InlineData("   alberto fernandez ", "77141516")]
    public void CreateValidPatient(string name, string phone)
    {
        var patient = new Patient(name, phone);

        Assert.NotNull(patient);
        Assert.Equal(patient.Name.NormalizeName(name), patient.Name);
        Assert.Equal(phone, patient.Phone);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("", "77141516")]
    public void CreateEmptyNamePatient(string name, string phone)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Administrator(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("name (Parameter 'Full name cannot be empty')", exception.Message);
    }

    [Theory]
    [InlineData("6666", "67141516")]
    [InlineData("alberto.fernandez", "67141516")]
    public void CreateInvalidNamePatient(string name, string phone)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Administrator(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Full name can only contain letters and spaces (Parameter 'name')", exception.Message);
    }

    [Fact]
    public void CreateEmptyPhoneAdministator()
    {
        string name = "Carlos Clavijo";
        string phone = "";

        var exception = Assert.Throws<ArgumentNullException>(() => new Administrator(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("phone (Parameter 'Phone number cannot be empty')", exception.Message);
    }

    [Theory]
    [InlineData("Alberto Fernandez ", "7714151")]
    [InlineData("alberto fernandez  ", "7714151.")]
    public void CreatsdeInvalidPhoneAdministator(string name, string phone)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Administrator(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Phone number can only contain numbers and must be 8 characters long (Parameter 'phone')", exception.Message);
    }
}
