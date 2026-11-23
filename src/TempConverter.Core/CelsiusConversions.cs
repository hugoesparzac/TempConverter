namespace TempConverter.Core;

public static class CelsiusConversions
{
    public static double ToFahrenheit(double celsius) =>
        TemperatureConverter.Convert(celsius, TemperatureScale.Celsius, TemperatureScale.Fahrenheit);
}
