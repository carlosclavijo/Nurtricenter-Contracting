using Contracting.Domain.Administrators;

namespace Contracting.Test.Domain.Administrators;

public class AdministratorTest
{

    [Theory]
    [InlineData("carlos clavijo", "71234568")]
    [InlineData("Carlos Clavijo", "77141516")]
    [InlineData("Carlos    Clavijo", "77141516")]
    [InlineData("   carlos clavijo ", "77141516")]
    public void CreateValidAdministrator(string name, string phone)
    {
        var administrator = new Administrator(name, phone);

        Assert.NotNull(administrator);
        Assert.Equal(administrator.Name.NormalizeName(name), administrator.Name);
        Assert.Equal(phone, administrator.Phone);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("", "77141516")]
    public void CreateEmptyNameAdministator(string name, string phone)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Administrator(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("name (Parameter 'Full name cannot be empty')", exception.Message);
    }

    [Theory]
    [InlineData("7777", "77141516")]
    [InlineData("carlos.clavijo", "77141516")]
    public void CreateInvalidNameAdministator(string name, string phone)
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
    [InlineData("Carlos Clavijo", "7714151")] 
    [InlineData("carlos clavijo", "7714151.")]
    public void CreatsdeInvalidPhoneAdministator(string name, string phone)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Administrator(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Phone number can only contain numbers and must be 8 characters long (Parameter 'phone')", exception.Message);
    }
}
