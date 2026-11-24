namespace TempConverter.Core;

public static class TemperatureScaleExtensions
{
    public static string GetSymbol(this TemperatureScale scale) => scale switch
    {
        TemperatureScale.Celsius => "°C",
        TemperatureScale.Fahrenheit => "°F",
        TemperatureScale.Kelvin => "K",
        TemperatureScale.Rankine => "°R",
        _ => throw new ArgumentOutOfRangeException(nameof(scale), scale, null),
    };

    public static double GetAbsoluteZeroMinimum(this TemperatureScale scale) => scale switch
    {
        TemperatureScale.Celsius => -273.15,
        TemperatureScale.Fahrenheit => -459.67,
        TemperatureScale.Kelvin => 0.0,
        TemperatureScale.Rankine => 0.0,
        _ => throw new ArgumentOutOfRangeException(nameof(scale), scale, null),
    };
}
