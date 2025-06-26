using Contracting.Domain.Shared;

namespace Contracting.Test.Domain.Shared;

public class PhoneNumberValueTest
{
    [Fact]
    public void CreateValidPhoneNumbeR()
    {
        string number = "77141516";

        PhoneNumberValue value = new(number);

        Assert.Equal(number, value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreateEmptyOrNullPhoneNumber(string? number)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new PhoneNumberValue(number));

        Assert.Equal("phone (Parameter 'Phone number cannot be empty')", exception.Message);
    }

    [Theory]
    [InlineData("57897897")]//8 size lenth but withou beggining with 6 or 7
    [InlineData("789789787")]//9 size long
    [InlineData("78078078S")]//Not numeric characters
    public void CreateInvalidPhoneNumber(string number)
    {
        var exception = Assert.Throws<ArgumentException>(() => new PhoneNumberValue(number));

        Assert.Equal("Phone number can only contain numbers and must be 8 characters long (Parameter 'phone')", exception.Message);
    }
}
