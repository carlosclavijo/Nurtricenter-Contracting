using Contracting.Domain.Abstractions;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Administrators;

public class Administrator : AggregateRoot
{
    public FullNameValue Name { get; set; }
    public PhoneNumberValue Phone { get; set; }

    public Administrator(string name, string phone) : base(Guid.NewGuid())
    {
        Name = name;
        Phone = phone;
    }

    //Need for EF Core
    private Administrator() { }
}