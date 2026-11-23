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
}
