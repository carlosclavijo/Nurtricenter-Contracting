using System;
using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;

namespace Contracting.Test.Domain.Contracts;

public class ContractTest
{
    [Fact]
    public void CreateValidHalfMonthContract()
    {
        Guid administratorId = Guid.Parse("fb23ac07-70d6-4302-adb9-de2831b6aae9");
        Guid patientId = Guid.Parse("5f91130c-44ec-432f-b862-9879d8c50298");
        ContractType type = ContractType.HalfMonth;
        DateTime start = DateTime.Now.AddDays(2);

        Contract contract = new(administratorId, patientId, type, start);

        Assert.NotNull(contract);
        Assert.Equal(contract.AdministratorId, administratorId);
        Assert.Equal(contract.PatientId, patientId);
        Assert.Equal(contract.Type, Enum.TryParse<ContractType>("HalfMonth", true, out ContractType result) ? result : throw new ArgumentException($"Invalid contract type: {type}"));
        Assert.Equal(contract.StartDate.ToUniversalTime(), start.ToUniversalTime());
    }

    [Fact]
    public void CreateValidFullMonthContract()
    {
        Guid administratorId = Guid.Parse("fb23ac07-70d6-4302-adb9-de2831b6aae9");
        Guid patientId = Guid.Parse("5f91130c-44ec-432f-b862-9879d8c50298");
        ContractType type = ContractType.FullMonth;
        DateTime start = DateTime.Now.AddDays(2);

        Contract contract = new(administratorId, patientId, type, start);

        Assert.NotNull(contract);
        Assert.Equal(contract.AdministratorId, administratorId);
        Assert.Equal(contract.PatientId, patientId);
        Assert.Equal(contract.Type, Enum.TryParse<ContractType>("FullMonth", true, out ContractType result) ? result : throw new ArgumentException($"Invalid contract type: {type}"));
        Assert.Equal(contract.StartDate.ToUniversalTime(), start.ToUniversalTime());
    }

    [Fact]
    public void CalculateFullMonthCost()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);
        
        var type = ContractType.FullMonth;

        decimal cost = contract.CalculateTotalCost(type);

        Assert.Equal(1000, cost);
    }

    [Fact]
    public void CalculateHalfMonthCost()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);
        
        var type = ContractType.HalfMonth;

        decimal cost = contract.CalculateTotalCost(type);

        Assert.Equal(500, cost);
    }

    [Fact]
    public void CreateValidCalendar()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);
        var contractId1 = Guid.NewGuid();
        var date1 = DateTime.Today;
        var street1 = "Any Street";
        var number1 = 30;
        var longitude1 = -74.0060;
        var latitude1 = 40.7128;
        DeliveryDay day1 = new DeliveryDay(contractId1, date1, street1, number1, longitude1, latitude1);

        var contractId2 = Guid.NewGuid();
        var date2 = DateTime.Today.AddDays(2);
        var street2 = "Other Street";
        var number2 = 70;
        var longitude2 = -31.0060;
        var latitude2 = 29.7456;
        DeliveryDay day2 = new DeliveryDay(contractId2, date2, street2, number2, longitude2, latitude2);

        List<DeliveryDay> listDays = [day1, day2];
        contract.CreateCalendar(listDays);

        Assert.NotNull(listDays);
        Assert.NotNull(contract.DeliveryDays);
        Assert.Equal(2, listDays.Count);
        Assert.Equal(2, contract.DeliveryDays.Count);
        Assert.Contains(listDays[0], contract.DeliveryDays);
        Assert.Contains(listDays[1], contract.DeliveryDays);
    }

    [Fact]
    public void CreateInvalidCalendar()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);
        List<DeliveryDay> deliveryDays = new List<DeliveryDay>();

        var exception = Assert.Throws<ArgumentNullException>(() => contract.CreateCalendar(deliveryDays));

        Assert.NotNull(exception);
        Assert.Equal("days (Parameter 'Days cannot be null')", exception.Message);
    }

    public List<DeliveryDay> ListDays(Guid id)
    {
        var date = DateTime.Today;
        var street = "Any Street";
        var number = 30;
        var longitude = -74.0060;
        var latitude = 40.7128;
        DeliveryDay day = new DeliveryDay(id, date, street, number, longitude, latitude);
        List<DeliveryDay> days = new List<DeliveryDay>();

        for (int i = 0; i < 10; i++)
        {
            DeliveryDay newDay = new DeliveryDay(id, date.AddDays(i), street, number, longitude, latitude);
            days.Add(newDay);
        }

        return days;
    }

    [Fact]
    public void UpdateAddressByDaysTest()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);
        contract.CreateCalendar(ListDays(contract.Id));
        DateTime tomorrow = DateTime.Today.AddDays(1);
        DateTime afterTomorrow = DateTime.Today.AddDays(2);
        var street = "Different Street";
        var number = 10;
        var longitude = -18.2938;
        var latitude = 57.9183;
        DeliveryDay updateDay1 = new DeliveryDay(contract.Id, tomorrow, street, number, longitude, latitude);
        DeliveryDay updateDay2 = new DeliveryDay(contract.Id, afterTomorrow, street, number, longitude, latitude);

        contract.UpdateAddressByDays(tomorrow, afterTomorrow, street, number, longitude, latitude);

        Assert.Equal(updateDay1.Date.Kind, contract.DeliveryDays.ElementAt(1).Date.Kind);
        Assert.Equal(updateDay2.Date.Kind, contract.DeliveryDays.ElementAt(2).Date.Kind);
    }

    [Fact]
    public void InProgressValidTest()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);

        contract.InProgress();

        Assert.Equal(ContractStatus.InPropgress, contract.Status);
    }

    [Fact]
    public void InProgressInValidTest()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);

        contract.InProgress();

        var exception = Assert.Throws<InvalidOperationException>(() => contract.InProgress());

        Assert.Equal("Cannot progress without creating a contract", exception.Message);
    }

    [Fact]
    public void CompleteValidTest()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);

        contract.InProgress();
        contract.Complete();

        Assert.Equal(ContractStatus.Completed, contract.Status);
    }

    [Fact]
    public void CompleteInValidTest()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);

        var exception = Assert.Throws<InvalidOperationException>(() => contract.Complete());

        Assert.Equal("Cannot complete without contract beign in progress contract", exception.Message);
    }
}
