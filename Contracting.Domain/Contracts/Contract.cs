using Contracting.Domain.Abstractions;
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
        _deliveryDays = new List<DeliveryDay>();

		//AddDomainEvent(new ContractCreated(Id, PatientId, StartDate, Type.ToString()));
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
        if (!days.Any())
        {
            throw new ArgumentNullException("Days cannot be null", nameof(days));
        }
        _deliveryDays = days;
    }

    public void UpdateAddressByDays(DateTime fromDate, DateTime toDate, string street, int number, double latitude, double longitude)
    {
        fromDate = fromDate.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(fromDate, DateTimeKind.Utc) : fromDate.ToUniversalTime();
        toDate = toDate.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(toDate, DateTimeKind.Utc) : toDate.ToUniversalTime();
        if (fromDate < DateTime.Today.AddDays(1))
        {
            throw new ArgumentException("Date has to be day after tomorrow at least", nameof(fromDate));
        }
        if (fromDate.Date >= toDate.Date)
        {
            throw new ArgumentException("ToDate cannot be before than FromDate", nameof(toDate));
        }
        for (int i = 0; i < _deliveryDays.Count; i++)
        {
            if (_deliveryDays[i].Date >= fromDate.Date && _deliveryDays[i].Date <= toDate.Date)
            {
                _deliveryDays[i] = new DeliveryDay(Id, _deliveryDays[i].Date, street, number, latitude, longitude);
            }
        }
    }

    public void CancelDate(DateTime date)
    {
        if (date < DateTime.Today.AddDays(2))
        {
            throw new ArgumentException("Date has to be day after tomorrow at least", nameof(date));
        }
        for (int i = 0; i < _deliveryDays.Count; i++)
        {
            if (_deliveryDays[i].Date == date.Date)
            {
                _deliveryDays.RemoveAt(i);
            }
        }
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