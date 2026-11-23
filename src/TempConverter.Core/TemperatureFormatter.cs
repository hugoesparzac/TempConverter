using System.Globalization;

namespace TempConverter.Core;

public static class TemperatureFormatter
{
    public static string Format(double value) => Format(value, 2);

    public static string Format(double value, int precision)
    {
        if (precision is < 0 or > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(precision), "Precision must be between 0 and 6 decimal places.");
        }

        string specifier = precision == 0
            ? "0"
            : "0." + new string('#', precision);
        string formatted = value.ToString(specifier, CultureInfo.InvariantCulture);
        return formatted == "-0" ? "0" : formatted;
    }

    public static string FormatOperand(double value) => value.ToString("0.#####", CultureInfo.InvariantCulture);
}
