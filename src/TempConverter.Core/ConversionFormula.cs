namespace TempConverter.Core;

public static class ConversionFormula
{
    public static string Render(double value, TemperatureScale source, TemperatureScale target)
    {
        string substituted = TemperatureFormatter.Format(value);
        string result = TemperatureFormatter.Format(TemperatureConverter.Convert(value, source, target));

        return (source, target) switch
        {
            (TemperatureScale.Celsius, TemperatureScale.Fahrenheit) => $"°F = {substituted} × 9/5 + 32 = {result}",
            (TemperatureScale.Celsius, TemperatureScale.Kelvin) => $"K = {substituted} + 273.15 = {result}",
            (TemperatureScale.Celsius, TemperatureScale.Rankine) => $"°R = ({substituted} + 273.15) × 9/5 = {result}",
            (TemperatureScale.Fahrenheit, TemperatureScale.Celsius) => $"°C = ({substituted} - 32) × 5/9 = {result}",
            (TemperatureScale.Fahrenheit, TemperatureScale.Kelvin) => $"K = ({substituted} - 32) × 5/9 + 273.15 = {result}",
            (TemperatureScale.Fahrenheit, TemperatureScale.Rankine) => $"°R = {substituted} + 459.67 = {result}",
            (TemperatureScale.Kelvin, TemperatureScale.Celsius) => $"°C = {substituted} - 273.15 = {result}",
            (TemperatureScale.Kelvin, TemperatureScale.Fahrenheit) => $"°F = ({substituted} - 273.15) × 9/5 + 32 = {result}",
            (TemperatureScale.Kelvin, TemperatureScale.Rankine) => $"°R = {substituted} × 9/5 = {result}",
            (TemperatureScale.Rankine, TemperatureScale.Celsius) => $"°C = {substituted} × 5/9 - 273.15 = {result}",
            (TemperatureScale.Rankine, TemperatureScale.Fahrenheit) => $"°F = {substituted} - 459.67 = {result}",
            (TemperatureScale.Rankine, TemperatureScale.Kelvin) => $"K = {substituted} × 5/9 = {result}",
            _ => string.Empty,
        };
    }
}
