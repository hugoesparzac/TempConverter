using System.Globalization;

namespace TempConverter.Core;

public static class TemperatureValidator
{
    public static TemperatureValidationOutcome Validate(string text, TemperatureScale scale) =>
        TryParseValue(text, out double value)
            ? Validate(value, scale)
            : TemperatureValidationOutcome.NotANumber;

    public static TemperatureValidationOutcome Validate(double value, TemperatureScale scale) =>
        value >= scale.GetAbsoluteZeroMinimum()
            ? TemperatureValidationOutcome.Valid
            : TemperatureValidationOutcome.BelowAbsoluteZero;

    public static bool TryParseValue(string text, out double value)
    {
        const NumberStyles AllowedStyles = NumberStyles.AllowLeadingWhite
            | NumberStyles.AllowTrailingWhite
            | NumberStyles.AllowLeadingSign
            | NumberStyles.AllowDecimalPoint;
        bool parsed = double.TryParse(text.Replace(',', '.'), AllowedStyles, CultureInfo.InvariantCulture, out value);
        return parsed && double.IsFinite(value);
    }
}
