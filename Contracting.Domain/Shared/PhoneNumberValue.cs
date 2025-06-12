using System.Text.RegularExpressions;

namespace Contracting.Domain.Shared;

public record PhoneNumberValue
{
    private static readonly Regex PhoneRegex = new(@"^[67]\d{7}$", RegexOptions.Compiled);
    
    public string Phone { get; init; }

    public PhoneNumberValue(string phone)
    {
        if (string.IsNullOrEmpty(phone))
        {
            throw new ArgumentNullException("Phone number cannot be empty", nameof(phone));
        }

        if (!PhoneRegex.IsMatch(phone))
        {
            throw new ArgumentException("Phone number can only contain numbers and must be 8 characters long", nameof(phone));
        }
        Phone = phone;
    }

    public static implicit operator string(PhoneNumberValue phone)
    {
        return phone == null ? "" : phone.Phone;
    }

    public static implicit operator PhoneNumberValue(string phone)
    {
        return new PhoneNumberValue(phone);
    }

}
