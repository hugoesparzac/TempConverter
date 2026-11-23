namespace TempConverter.Core;

public static class TemperatureConverter
{
    public static double Convert(double value, TemperatureScale source, TemperatureScale target)
    {
        double celsius = ToCelsius(value, source);
        return FromCelsius(celsius, target);
    }

    private static double ToCelsius(double value, TemperatureScale scale) => scale switch
    {
        TemperatureScale.Celsius => value,
        TemperatureScale.Fahrenheit => (value - 32.0) * 5.0 / 9.0,
        TemperatureScale.Kelvin => value - 273.15,
        TemperatureScale.Rankine => (value - 491.67) * 5.0 / 9.0,
        _ => throw new ArgumentOutOfRangeException(nameof(scale), scale, null),
    };

    private static double FromCelsius(double celsius, TemperatureScale scale) => scale switch
    {
        TemperatureScale.Celsius => celsius,
        TemperatureScale.Fahrenheit => celsius * 9.0 / 5.0 + 32.0,
        TemperatureScale.Kelvin => celsius + 273.15,
        TemperatureScale.Rankine => (celsius + 273.15) * 9.0 / 5.0,
        _ => throw new ArgumentOutOfRangeException(nameof(scale), scale, null),
    };
}
