using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contracting.Domain.Abstractions;

public abstract record DomainEvent : INotification
{
    public Guid Id { get; set; }
    public DateTime OcurredOn { get; set; }

    public DomainEvent()
    {
        Id = Guid.NewGuid();
        OcurredOn = DateTime.Now;
    }
}
