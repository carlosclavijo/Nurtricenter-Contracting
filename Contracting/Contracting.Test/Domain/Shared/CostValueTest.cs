using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Shared;

namespace Contracting.Test.Domain.Shared;

public class CostValueTest
{
    [Fact]
    public void CostValueWithPositiveValue()
    {
        decimal expected = 5.5m;

        var costValue = new CostValue(expected);

        Assert.Equal(expected, costValue.Value);
    }

    [Fact]
    public void CostValueWithNegativeValue()
    {
        decimal negative = -5.5m;

        var exception = Assert.Throws<ArgumentException>(() => new CostValue(negative));

        Assert.Equal("Cost value cannot be negative (Parameter 'value')", exception.Message);
    }
}
