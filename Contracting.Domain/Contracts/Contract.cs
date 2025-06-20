using Contracting.Domain.Abstractions;
using Contracting.Domain.Contracts.Events;
using Contracting.Domain.Delivery;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Contracts;

public class Contract : AggregateRoot
{
    public Guid AdministratorId { get; set; }
    public Guid PatientId { get; set; }
    public ContractType Type { get; set; }
    public ContractStatus Status { get; set; }
    private DateTime _creationDate;
    public DateTime CreationDate
    {
        get => _creationDate;
        set => _creationDate = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
    }
    private DateTime _startDate;
    public DateTime StartDate
    {
        get => _startDate;
        set => _startDate = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
    }
    private DateTime _completedDate;
    public DateTime? CompletedDate
    {
        get => _completedDate;
        set => _completedDate = (DateTime)(value.HasValue && value.Value.Kind != DateTimeKind.Utc ? value.Value.ToUniversalTime() : value);
    }
    public CostValue Cost { get; set; }
    private List<DeliveryDay> _deliveryDays;
    public ICollection<DeliveryDay> DeliveryDays
    {
        get
        {
            return _deliveryDays;
        }
    }

    public Contract(Guid administratorId, Guid patientId, ContractType type, DateTime startDate) : base(Guid.NewGuid())
    {
        AdministratorId = administratorId;
        PatientId = patientId;
        Type = type;
        Status = ContractStatus.Created;
        CreationDate = DateTime.Now;
        StartDate = startDate;
        Cost = CalculateTotalCost(type);
        _deliveryDays = [];
	}

	public decimal CalculateTotalCost(ContractType type)
    {
        if (type == ContractType.FullMonth)
        {
            return 1000;
        }
        return 500;
    }

    public void CreateCalendar(List<DeliveryDay> days)
    {
        if (days.Count == 0)
        {
            throw new ArgumentNullException(nameof(days), "Days cannot be null");
        }
        _deliveryDays = days;

		AddDomainEvent(new CalendarCreated(Id, PatientId, StartDate, days[^1].Date, _deliveryDays));
	}

    public void InProgress()
    {
        if (Status != ContractStatus.Created)
        {
            throw new InvalidOperationException("Cannot progress without creating a contract");
        }
        Status = ContractStatus.InPropgress;
    }

    public void Complete()
    {
        if (Status != ContractStatus.InPropgress)
        {
            throw new InvalidOperationException("Cannot complete without contract beign in progress contract");
        }
        Status = ContractStatus.Completed;
        CompletedDate = DateTime.Now;
    }

    private Contract() { }
}