using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Patients;

namespace Contracting.Test.Domain.Patients;

public class PatientTest
{
    [Fact]
    public void CreateValidPatient()
    {
        string name = "Jose Miguel Alvarez";
        string phone = "77887766";

        var patient = new Patient(name, phone);

        Assert.Equal(name, patient.Name);
        Assert.Equal(phone, patient.Phone);
    }
}
