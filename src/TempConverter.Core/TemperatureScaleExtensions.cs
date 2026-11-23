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
}
