using Contracting.Domain.Contracts;

namespace Contracting.Test.Domain.Contracts;

public class ContractFactoryTest
{
    private readonly ContractFactory _factory = new();

    [Fact]
    public void CreateValidFullMonthContractFactory()
    {
        Guid administratorId = Guid.NewGuid();
        Guid patientId = Guid.NewGuid();
        DateTime time = DateTime.UtcNow;
        var contract = _factory.CreateFullMonthContract(administratorId, patientId, time);

        Assert.Equal(administratorId, contract.AdministratorId);
        Assert.Equal(patientId, contract.PatientId);
        Assert.Equal(time.ToUniversalTime().Ticks / TimeSpan.TicksPerSecond, contract.CreationDate.Ticks / TimeSpan.TicksPerSecond);
    }

    [Fact]
    public void CreateValidHalfMonthContractFactory()
    {
        Guid administratorId = Guid.NewGuid();
        Guid patientId = Guid.NewGuid();
        DateTime time = DateTime.UtcNow;
        var contract = _factory.CreateHalfMonthContract(administratorId, patientId, time);

        Assert.Equal(administratorId, contract.AdministratorId);
        Assert.Equal(patientId, contract.PatientId);
        Assert.Equal(time.ToUniversalTime().Ticks / TimeSpan.TicksPerSecond, contract.CreationDate.Ticks / TimeSpan.TicksPerSecond);
    }

    [Fact]
    public void CreateEmptyIdFullMonthContracts()
    {
        Guid administratorId = Guid.Empty;
        Guid patientId = Guid.NewGuid();
        DateTime time = DateTime.Now;
        var exception = Assert.Throws<ArgumentNullException>(() => _factory.CreateFullMonthContract(administratorId, patientId, time));

        Assert.Equal("administratorId (Parameter 'AdministratorId is required')", exception.Message);

        administratorId = Guid.NewGuid();
        patientId = Guid.Empty;
        exception = Assert.Throws<ArgumentNullException>(() => _factory.CreateFullMonthContract(administratorId, patientId, time));

        Assert.Equal("patientId (Parameter 'PatientId is required')", exception.Message);
    }

    [Fact]
    public void CreateEmptyIdHalfMonthContracts()
    {
        Guid administratorId = Guid.Empty;
        Guid patientId = Guid.NewGuid();
        DateTime time = DateTime.Now;
        var exception = Assert.Throws<ArgumentNullException>(() => _factory.CreateHalfMonthContract(administratorId, patientId, time));

        Assert.Equal("administratorId (Parameter 'AdministratorId is required')", exception.Message);

        administratorId = Guid.NewGuid();
        patientId = Guid.Empty;
        exception = Assert.Throws<ArgumentNullException>(() => _factory.CreateHalfMonthContract(administratorId, patientId, time));

        Assert.Equal("patientId (Parameter 'PatientId is required')", exception.Message);
    }

}
