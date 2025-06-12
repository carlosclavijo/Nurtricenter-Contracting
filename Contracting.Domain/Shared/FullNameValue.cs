using System.Globalization;
using System.Text.RegularExpressions;

namespace Contracting.Domain.Shared;

public record FullNameValue
{
    private static readonly Regex NameRegex = new(@"^[a-zA-ZÀ-ÿ\s]+$", RegexOptions.Compiled);
    private const int MaxLength = 100;

    public string Name { get; init; }

    public FullNameValue(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("Full name cannot be empty", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Full name cannot exceed {MaxLength} characters", nameof(name));
        }

        if (!NameRegex.IsMatch(name))
        {
            throw new ArgumentException("Full name can only contain letters and spaces", nameof(name));
        }

        Name = NormalizeName(name);
    }

    public static implicit operator string(FullNameValue name)
    {
        return name == null ? "" : name.Name;
    }

    public static implicit operator FullNameValue(string name)
    {
        return new FullNameValue(name);
    }

    public string NormalizeName(string name)
    {
        name = Regex.Replace(name.Trim(), @"\s+", " ");

        name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

        return name;
    }
}
