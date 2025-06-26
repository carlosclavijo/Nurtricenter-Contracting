using Contracting.Domain.Abstractions;
using Contracting.Domain.DeliveryDays.Events;

namespace Contracting.Domain.Delivery;

public class DeliveryDay : Entity
{
    public Guid ContractId { get; set; }
    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => _date = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
    }
    public string Street { get; private set; }
    public int Number { get; private set; }

    public DeliveryDay(Guid contractId, DateTime date, string street, int number) : base(Guid.NewGuid())
    {
        ContractId = contractId;
        Date = date.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(date, DateTimeKind.Utc) : date.ToUniversalTime();
        Street = street;
        Number = number;
    }

    public void Update(string street, int number)
    {
        Street = street;
        Number = number;

		AddDomainEvent(new DeliveryDayUpdated(ContractId, Id, street, number));
	}

	public void Delete()
	{
		AddDomainEvent(new DeliveryDayDeleted(ContractId, Id));
	}

    private DeliveryDay() { }
}