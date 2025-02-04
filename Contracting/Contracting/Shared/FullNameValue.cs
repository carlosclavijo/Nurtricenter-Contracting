using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            throw new ArgumentException("Full name cannot be empty", nameof(name));
        }

        name = Regex.Replace(name.Trim(), @"\s+", " ");

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Full name cannot exceed {MaxLength} characters", nameof(name));
        }

        if (!NameRegex.IsMatch(name))
        {
            throw new ArgumentException("Full name can only contain letters and spaces", nameof(name));
        }

        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
    }

    public static implicit operator string(FullNameValue name)
    {
        return name == null ? "" : name.Name;
    }

    public static implicit operator FullNameValue(string name)
    {
        return new FullNameValue(name);
    }
}
