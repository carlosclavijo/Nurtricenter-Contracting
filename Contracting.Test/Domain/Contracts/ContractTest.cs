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
        DeliveryDay day1 = new(contractId1, date1, street1, number1);

        var contractId2 = Guid.NewGuid();
        var date2 = DateTime.Today.AddDays(2);
        var street2 = "Other Street";
        var number2 = 70;
        DeliveryDay day2 = new(contractId2, date2, street2, number2);

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
        List<DeliveryDay> deliveryDays = [];

        var exception = Assert.Throws<ArgumentNullException>(() => contract.CreateCalendar(deliveryDays));

        Assert.NotNull(exception);
        Assert.Equal("Days cannot be null (Parameter 'days')", exception.Message);
    }

    public static List<DeliveryDay> ListDays(Guid id)
    {
        var date = DateTime.Today;
        var street = "Any Street";
        var number = 30;
        List<DeliveryDay> days = new List<DeliveryDay>();

        for (int i = 0; i < 10; i++)
        {
            DeliveryDay newDay = new(id, date.AddDays(i), street, number);
            days.Add(newDay);
        }

        return days;
    }

    [Fact]
    public void InProgressValidTest()
    {
        Contract contract = new(Guid.NewGuid(), Guid.NewGuid(), ContractType.FullMonth, DateTime.Now);
        contract.InProgress();
        Assert.Equal(ContractStatus.InProgress, contract.Status);
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
