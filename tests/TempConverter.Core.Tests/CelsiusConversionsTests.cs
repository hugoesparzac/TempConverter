namespace TempConverter.Core.Tests;

public class CelsiusConversionsTests
{
    [Theory]
    [InlineData(37.0, 98.6)]
    [InlineData(0.0, 32.0)]
    [InlineData(100.0, 212.0)]
    [InlineData(-40.0, -40.0)]
    public void ToFahrenheit_StandardPoints_ReturnExactEquivalent(double celsius, double expected)
    {
        double actual = CelsiusConversions.ToFahrenheit(celsius);

        Assert.Equal(expected, actual, precision: 10);
    }

    [Fact]
    public void ToFahrenheit_AbsoluteZero_ReturnsKnownEquivalent()
    {
        double actual = CelsiusConversions.ToFahrenheit(-273.15);

        Assert.Equal(-459.67, actual, precision: 10);
    }
}
