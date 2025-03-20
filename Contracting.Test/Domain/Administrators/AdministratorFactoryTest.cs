using System;
using Contracting.Domain.Administrators;

namespace Contracting.Test.Domain.Administrators;

public class AdministratorFactoryTest
{
    private readonly AdministratorFactory _factory = new();

    [Fact]
    public void CreateValidAdministratorFactory()
    {
        string name = "Carlos Clavijo";
        string phone = "77661415";

        var administrator = _factory.Create(name, phone);

        Assert.NotNull(administrator);
        Assert.Equal(administrator.Name.NormalizeName(name), administrator.Name);
        Assert.Equal(phone, administrator.Phone);
    }

    [Fact]
    public void CreateInvalidNameAdministratorFactory()
    {
        string name = "";
        string phone = "7141516";


        var exception = Assert.Throws<ArgumentException>(() => _factory.Create(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Administrator name is required (Parameter 'administratorName')", exception.Message);
    }

    [Fact]
    public void CreateInvalidPhoneAdministratorFactory()
    {
        string name = "Carlos Clavijo";
        string phone = "";

        var exception = Assert.Throws<ArgumentException>(() => _factory.Create(name, phone));

        Assert.NotNull(exception);
        Assert.Equal("Administrator phone is required (Parameter 'administratorPhone')", exception.Message);
    }
}

