using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;
using Contracting.Domain.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contracting.Domain.Delivery;

public class DeliveryDay : Entity
{
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }
    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => _date = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
    }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public double Longitude { get; private set; }
    public double Latitude { get; private set; }


    public DeliveryDay(Guid contractId, DateTime date, string street, int number, double longitude, double latitude) : base(Guid.NewGuid())
    {
        ContractId = contractId;
        Date = date;
        Street = street;
        Number = number;
        Longitude = longitude;
        Latitude = latitude;
    }

    public void Update(string street, int number, double longitude, double latitude)
    {
        Street = street;
        Number = number;
        Longitude = longitude;
        Latitude = latitude;
    }

    private DeliveryDay() { }
}