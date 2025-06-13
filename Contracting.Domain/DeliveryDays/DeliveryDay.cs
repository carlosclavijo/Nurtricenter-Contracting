using System.Text.Json.Serialization;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts;

namespace Contracting.Domain.Delivery;

public class DeliveryDay : Entity
{
    public Guid ContractId { get; set; }
	//public Contract Contract { get; set; }
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
        Date = date.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(date, DateTimeKind.Utc) : date.ToUniversalTime();
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