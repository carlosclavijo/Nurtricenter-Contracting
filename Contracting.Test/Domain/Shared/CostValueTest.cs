using Contracting.Domain.Shared;

namespace Contracting.Test.Domain.Shared;

public class CostValueTest
{
    [Fact]
    public void CostValueWithPositiveValue()
    {
        decimal expected = 5.5m;

        var costValue = expected;

        Assert.True(costValue.Equals(expected));
        Assert.Equal(expected, costValue);
    }

    [Fact]
    public void CostValueWithNegativeValue()
    {
        decimal negative = -5.5m;

        var exception = Assert.Throws<ArgumentException>(() => new CostValue(negative));

        Assert.NotNull(exception);
        Assert.Equal("Cost value cannot be negative (Parameter 'value')", exception.Message);
    }

    [Fact]
    public void CostValueWithNullValue()
    {
        CostValue cost = null;

        decimal value = cost; // Se convierte implícitamente a decimal

        Assert.Equal(0m, value);
    }
}
