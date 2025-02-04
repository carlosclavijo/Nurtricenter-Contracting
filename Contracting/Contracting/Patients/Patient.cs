using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Patients;

public class Patient : AggregateRoot
{
    public FullNameValue Name { get; private set; }
    public PhoneNumberValue Phone { get; private set; }

    public Patient(string name, string phone) : base(Guid.NewGuid())
    {
        Name = name;
        Phone = phone;
    }

    //Need for EF Core
    private Patient() { }
}