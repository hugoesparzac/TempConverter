namespace TempConverter.Core;

public static class CelsiusConversions
{
    public static double ToFahrenheit(double celsius) => celsius * 9.0 / 5.0 + 32.0;
}
