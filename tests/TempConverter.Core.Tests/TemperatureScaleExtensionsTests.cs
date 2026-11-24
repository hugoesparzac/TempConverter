namespace TempConverter.Core.Tests;

public class TemperatureScaleExtensionsTests
{
    [Theory]
    [InlineData(TemperatureScale.Celsius, "°C")]
    [InlineData(TemperatureScale.Fahrenheit, "°F")]
    [InlineData(TemperatureScale.Kelvin, "K")]
    [InlineData(TemperatureScale.Rankine, "°R")]
    public void GetSymbol_EachScale_ReturnsFormattedSymbol(TemperatureScale scale, string expected)
    {
        string actual = scale.GetSymbol();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, -273.15)]
    [InlineData(TemperatureScale.Fahrenheit, -459.67)]
    [InlineData(TemperatureScale.Kelvin, 0.0)]
    [InlineData(TemperatureScale.Rankine, 0.0)]
    public void GetAbsoluteZeroMinimum_EachScale_ReturnsScaleMinimum(TemperatureScale scale, double expected)
    {
        double actual = scale.GetAbsoluteZeroMinimum();

        Assert.Equal(expected, actual);
    }
}
