using System.Globalization;

namespace TempConverter.Core;

public static class TemperatureFormatter
{
    public static string Format(double value) => Format(value, 2);

    public static string Format(double value, int precision)
    {
        string specifier = precision == 0
            ? "0"
            : "0." + new string('#', precision);
        return value.ToString(specifier, CultureInfo.InvariantCulture);
    }

    public static string FormatOperand(double value) => value.ToString("0.#####", CultureInfo.InvariantCulture);
}
