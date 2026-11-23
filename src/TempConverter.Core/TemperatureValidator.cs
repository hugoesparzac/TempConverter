using System.Globalization;

namespace TempConverter.Core;

public static class TemperatureValidator
{
    public static TemperatureValidationOutcome Validate(string text, TemperatureScale scale)
    {
        if (!TryParse(text, out double value))
        {
            return TemperatureValidationOutcome.NotANumber;
        }

        return value >= scale.GetAbsoluteZeroMinimum()
            ? TemperatureValidationOutcome.Valid
            : TemperatureValidationOutcome.BelowAbsoluteZero;
    }

    private static bool TryParse(string text, out double value)
    {
        const NumberStyles AllowedStyles = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
        return double.TryParse(text.Replace(',', '.'), AllowedStyles, CultureInfo.InvariantCulture, out value);
    }
}
