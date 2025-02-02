using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;
using Contracting.Domain.Delivery;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Contracts;

public class Contract : AggregateRoot
{
    public Guid AdministratorId { get; private set; }
    public Guid PatientId { get; private set; }
    public ContractType Type { get; set; }
    public ContractStatus Status { get; set; }
    private DateTime _date;
    public DateTime CreationDate
    {
        get => _date;
        set => _date = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
    }
    public DateTime StartDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public CostValue Cost { get; private set; }
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
    }

    private decimal CalculateTotalCost(ContractType type)
    {
        if (type == ContractType.FullMonth)
        {
            return 1000;
        }
        else if (type == ContractType.HalfMonth)
        {
            return 500;
        }
        return 0;
    }

    public void CreateCalendar(List<DeliveryDay> days)
    {
        if (!days.Any())
        {
            throw new ArgumentNullException("Days cannot be null", nameof(days));
        }
        _deliveryDays = days;
    }

    public void UpdateAddresByDays(DateTime fromDate, DateTime toDate, string street, int number, double latitude, double longitude)
    {
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
            throw new InvalidOperationException("Cannot complete without creating a contract");
        }
        Status = ContractStatus.Completed;
        CompletedDate = DateTime.Now;
    }

    private Contract() { }
}