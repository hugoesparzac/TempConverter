namespace TempConverter.Core.Tests;

public class ConversionFormulaTests
{
    [Theory]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 37.0, "°F = 37 × 9/5 + 32 = 98.6")]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, 0.0, "K = 0 + 273.15 = 273.15")]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Rankine, 0.0, "°R = (0 + 273.15) × 9/5 = 491.67")]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, 32.0, "°C = (32 - 32) × 5/9 = 0")]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Kelvin, 212.0, "K = (212 - 32) × 5/9 + 273.15 = 373.15")]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Rankine, 32.0, "°R = 32 + 459.67 = 491.67")]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 273.15, "°C = 273.15 - 273.15 = 0")]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Fahrenheit, 273.15, "°F = (273.15 - 273.15) × 9/5 + 32 = 32")]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 100.0, "°R = 100 × 9/5 = 180")]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Celsius, 540.0, "°C = 540 × 5/9 - 273.15 = 26.85")]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Fahrenheit, 459.67, "°F = 459.67 - 459.67 = 0")]
    [InlineData(TemperatureScale.Rankine, TemperatureScale.Kelvin, 500.0, "K = 500 × 5/9 = 277.78")]
    public void Render_AllTwelveOrderedPairs_UsesDirectFormulaWithExactConstants(
        TemperatureScale source,
        TemperatureScale target,
        double value,
        string expected)
    {
        string actual = ConversionFormula.Render(value, source, target);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius)]
    [InlineData(TemperatureScale.Fahrenheit)]
    [InlineData(TemperatureScale.Kelvin)]
    [InlineData(TemperatureScale.Rankine)]
    public void Render_SameScalePairs_ReturnsEmpty(TemperatureScale scale)
    {
        string actual = ConversionFormula.Render(37.0, scale, scale);

        Assert.Equal(string.Empty, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, -40.0, "°F = -40 × 9/5 + 32 = -40")]
    [InlineData(TemperatureScale.Fahrenheit, TemperatureScale.Celsius, -40.0, "°C = (-40 - 32) × 5/9 = -40")]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Kelvin, -273.15, "K = -273.15 + 273.15 = 0")]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Celsius, 0.0, "°C = 0 - 273.15 = -273.15")]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 36.6, "°F = 36.6 × 9/5 + 32 = 97.88")]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 0.0049, "°F = 0.0049 × 9/5 + 32 = 32.01")]
    public void Render_BoundaryAndDecimalValues_SubstitutesValueAndResult(
        TemperatureScale source,
        TemperatureScale target,
        double value,
        string expected)
    {
        string actual = ConversionFormula.Render(value, source, target);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 100.0, "°F = 100 × 9/5 + 32 = 212")]
    [InlineData(TemperatureScale.Kelvin, TemperatureScale.Rankine, 200.0, "°R = 200 × 9/5 = 360")]
    public void Render_ResultTrimsTrailingZeros_MatchingFormatterRule(
        TemperatureScale source,
        TemperatureScale target,
        double value,
        string expected)
    {
        string actual = ConversionFormula.Render(value, source, target);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(37.0, TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 0, "°F = 37 × 9/5 + 32 = 99")]
    [InlineData(37.0, TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 6, "°F = 37 × 9/5 + 32 = 98.6")]
    [InlineData(36.6, TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 1, "°F = 36.6 × 9/5 + 32 = 97.9")]
    [InlineData(36.6, TemperatureScale.Celsius, TemperatureScale.Fahrenheit, 0, "°F = 36.6 × 9/5 + 32 = 98")]
    public void Render_WithPrecision_RoundsResultAndKeepsEnteredOperand(
        double value,
        TemperatureScale source,
        TemperatureScale target,
        int precision,
        string expected)
    {
        string actual = ConversionFormula.Render(value, source, target, precision);

        Assert.Equal(expected, actual);
    }
}
