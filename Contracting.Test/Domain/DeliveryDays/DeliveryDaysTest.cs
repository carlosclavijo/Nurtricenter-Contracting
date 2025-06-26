using Contracting.Domain.Delivery;

namespace Contracting.Test.Domain.DeliveryDays;

public class DeliveryDaysTest
{
    [Fact]
    public void CreateDeliveryDaysTest()
    {
        Guid contractId = Guid.NewGuid();
        DateTime date = DateTime.Now.ToUniversalTime();
        string street = "Street";
        int number = 10;
        var deliveryDay = new DeliveryDay(contractId, date, street, number);

        Assert.NotNull(deliveryDay);
        Assert.Equal(contractId, deliveryDay.ContractId);
        Assert.Equal(date, deliveryDay.Date);
        Assert.Equal(street, deliveryDay.Street);
    }

    [Fact]
    public void UpdateDeliveryDayTest()
    {
        Guid contractId = Guid.NewGuid();
        DateTime date = DateTime.Now.ToUniversalTime();
        string street = "Street";
        int number = 10;

        var deliveryDay = new DeliveryDay(contractId, date, street, number);

        Assert.NotNull(deliveryDay);
        Assert.Equal(contractId, deliveryDay.ContractId);
        Assert.Equal(date, deliveryDay.Date);
        Assert.Equal(street, deliveryDay.Street);

        string newStreet = "New Street";
        int newNumber = 20;

        deliveryDay.Update(newStreet, newNumber);

        Assert.Equal(newStreet, deliveryDay.Street);
        Assert.Equal(newNumber, deliveryDay.Number);
    }

	[Fact]
	public void DeleteDeliveryDayTest()
	{
		Guid contractId = Guid.NewGuid();
		DateTime date = DateTime.Now.ToUniversalTime();
		string street = "Street";
		int number = 10;

		var deliveryDay = new DeliveryDay(contractId, date, street, number);

		Assert.NotNull(deliveryDay);
		Assert.Equal(contractId, deliveryDay.ContractId);
		Assert.Equal(date, deliveryDay.Date);
		Assert.Equal(street, deliveryDay.Street);
		deliveryDay.Delete();
	}
}
