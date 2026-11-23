namespace TempConverter.Core.Tests;

public class TemperatureConverterTests
{
    [Theory]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 0.0, 32.0)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 100.0, 212.0)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, 0.0, 273.15)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, 100.0, 373.15)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Rankine, 0.0, 491.67)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Rankine, 100.0, 671.67)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, 32.0, 0.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, 212.0, 100.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, 32.0, 273.15)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, 212.0, 373.15)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Rankine, 32.0, 491.67)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Rankine, 212.0, 671.67)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 273.15, 0.0)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 373.15, 100.0)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 273.15, 32.0)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 373.15, 212.0)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 273.15, 491.67)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 373.15, 671.67)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Celsius, 491.67, 0.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Celsius, 671.67, 100.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Fahrenheit, 491.67, 32.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Fahrenheit, 671.67, 212.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Kelvin, 491.67, 273.15)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Kelvin, 671.67, 373.15)]
    public void Convert_AllOrderedPairs_StandardPoints_ReturnExpectedValues(
        TemperatureScale source,
        TemperatureScale target,
        double value,
        double expected)
    {
        double actual = TemperatureConverter.Convert(value, source, target);

        Assert.Equal(expected, actual, precision: 10);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, -40.0, -40.0)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 36.5, 97.7)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, -40.0, 233.15)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, 36.5, 309.65)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Rankine, -40.0, 419.67)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Rankine, 36.5, 557.37)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, -40.0, -40.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, -4.0, -20.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, 71.6, 22.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, -4.0, 253.15)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, 71.6, 295.15)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Rankine, -4.0, 455.67)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Rankine, 71.6, 531.27)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 100.0, -173.15)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 300.5, 27.35)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 100.0, -279.67)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 300.5, 81.23)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 100.0, 180.0)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 300.5, 540.9)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Celsius, 419.67, -40.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Celsius, 527.67, 20.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Fahrenheit, 419.67, -40.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Fahrenheit, 527.67, 68.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Kelvin, 419.67, 233.15)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Kelvin, 527.67, 293.15)]
    public void Convert_AllOrderedPairs_NegativeAndDecimalValues_ReturnExpectedValues(
        TemperatureScale source,
        TemperatureScale target,
        double value,
        double expected)
    {
        double actual = TemperatureConverter.Convert(value, source, target);

        Assert.Equal(expected, actual, precision: 10);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, -273.15, -459.67)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, -273.15, 0.0)]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Rankine, -273.15, 0.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, -459.67, -273.15)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, -459.67, 0.0)]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Rankine, -459.67, 0.0)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 0.0, -273.15)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 0.0, -459.67)]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 0.0, 0.0)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Celsius, 0.0, -273.15)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Fahrenheit, 0.0, -459.67)]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Kelvin, 0.0, 0.0)]
    public void Convert_AllOrderedPairs_AbsoluteZero_ReturnExpectedValues(
        TemperatureScale source,
        TemperatureScale target,
        double value,
        double expected)
    {
        double actual = TemperatureConverter.Convert(value, source, target);

        Assert.Equal(expected, actual, precision: 10);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, -40.5)]
    [InlineData(TemperatureScale.Fahrenheit, 72.4)]
    [InlineData(TemperatureScale.Kelvin, 300.5)]
    [InlineData(TemperatureScale.Rankine, 527.67)]
    public void Convert_SameScaleAsSourceAndTarget_ReturnsInputUnchanged(TemperatureScale scale, double value)
    {
        double actual = TemperatureConverter.Convert(value, scale, scale);

        Assert.Equal(value, actual, precision: 10);
    }
}
