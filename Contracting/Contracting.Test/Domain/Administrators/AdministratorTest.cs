using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Administrators;

namespace Contracting.Test.Domain.Administrators;

public class AdministratorTest
{
    [Fact]
    public void CreateValidAdministrator()
    {
        string name = "Carlos Clavijo";
        string phone = "77777777";

        var administrator = new Administrator(name, phone);

        Assert.Equal(name, administrator.Name);
        Assert.Equal(phone, administrator.Phone);
    }
}
