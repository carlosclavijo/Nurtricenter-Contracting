using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;

namespace Contracting.Domain.Administrators;

public class Administrator : AggregateRoot
{
    public string Name { get; private set; }
    public string Phone { get; private set; }

    public Administrator(string name, string phone) : base(Guid.NewGuid())
    {
        Name = name;
        Phone = phone;
    }

    //Need for EF Core
    private Administrator() { }
}