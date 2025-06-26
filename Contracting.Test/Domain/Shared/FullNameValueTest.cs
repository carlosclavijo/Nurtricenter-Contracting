using Contracting.Domain.Shared;

namespace Contracting.Test.Domain.Shared;

public class FullNameValueTest
{
    [Fact]
    public void FullNameWithValidCharacters()
    {
        string str = "carlos clavijo";

        FullNameValue name = str;

        Assert.Equal(name.NormalizeName(str), name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void FullNameWithNullOrEmptyValue(string? str)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new FullNameValue(str));

        Assert.Equal("name (Parameter 'Full name cannot be empty')", exception.Message);
    }

    [Fact]
    public void FullNameWithMoreThanHundredCharacters()
    {
        string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        var exception = Assert.Throws<ArgumentException>(() => new FullNameValue(str));
        Assert.Equal("Full name cannot exceed 100 characters (Parameter 'name')", exception.Message);
    }

    [Fact]
    public void FullNameWithInvalidCharacters()
    {
        string str = "!@#$%^&*()_+";

        var exception = Assert.Throws<ArgumentException>(() => new FullNameValue(str));
        Assert.Equal("Full name can only contain letters and spaces (Parameter 'name')", exception.Message);
    }

    [Fact]
    public void FullNameNormalizeName()
    {
        string name1 = "carlos clavijo";
        string name2 = "   aLBertO      fERNANDEZ";

        FullNameValue fullname1 = name1;
        FullNameValue fullname2 = name2;

        Assert.Equal("Carlos Clavijo", fullname1);
        Assert.Equal("Alberto Fernandez", fullname2);
    }
}
