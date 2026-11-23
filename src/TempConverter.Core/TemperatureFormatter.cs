using System.Globalization;

namespace TempConverter.Core;

public static class TemperatureFormatter
{
    public static string Format(double value) => value.ToString("0.##", CultureInfo.InvariantCulture);

    public static string FormatOperand(double value) => value.ToString("0.#####", CultureInfo.InvariantCulture);
}
