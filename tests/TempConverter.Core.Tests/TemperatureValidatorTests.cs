namespace TempConverter.Core.Tests;

public class TemperatureValidatorTests
{
    [Theory]
    [InlineData("abc")]
    [InlineData("12abc")]
    public void Validate_WhenTextIsNotANumber_ReturnsNotANumber(string text)
    {
        TemperatureValidationOutcome actual = TemperatureValidator.Validate(text, TemperatureScale.Celsius);

        Assert.Equal(TemperatureValidationOutcome.NotANumber, actual);
    }

    [Theory]
    [InlineData("NaN")]
    [InlineData("Infinity")]
    [InlineData("-Infinity")]
    public void Validate_WhenTextIsNonFiniteNumber_ReturnsNotANumber(string text)
    {
        TemperatureValidationOutcome actual = TemperatureValidator.Validate(text, TemperatureScale.Celsius);

        Assert.Equal(TemperatureValidationOutcome.NotANumber, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, "-300")]
    [InlineData(TemperatureScale.Fahrenheit, "-500")]
    [InlineData(TemperatureScale.Kelvin, "-1")]
    [InlineData(TemperatureScale.Rankine, "-1")]
    public void Validate_WhenValueIsBelowAbsoluteZero_ReturnsBelowMinimum(TemperatureScale scale, string text)
    {
        TemperatureValidationOutcome actual = TemperatureValidator.Validate(text, scale);

        Assert.Equal(TemperatureValidationOutcome.BelowAbsoluteZero, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, "-273.15")]
    [InlineData(TemperatureScale.Fahrenheit, "-459.67")]
    [InlineData(TemperatureScale.Kelvin, "0")]
    [InlineData(TemperatureScale.Rankine, "0")]
    public void Validate_WhenValueIsExactlyAtAbsoluteZero_ReturnsValid(TemperatureScale scale, string text)
    {
        TemperatureValidationOutcome actual = TemperatureValidator.Validate(text, scale);

        Assert.Equal(TemperatureValidationOutcome.Valid, actual);
    }

    [Theory]
    [InlineData(TemperatureScale.Celsius, "37")]
    [InlineData(TemperatureScale.Celsius, "-10")]
    [InlineData(TemperatureScale.Celsius, "36,6")]
    [InlineData(TemperatureScale.Fahrenheit, "-400")]
    [InlineData(TemperatureScale.Kelvin, "300.5")]
    [InlineData(TemperatureScale.Celsius, " 37")]
    [InlineData(TemperatureScale.Celsius, "37 ")]
    [InlineData(TemperatureScale.Celsius, ".5")]
    [InlineData(TemperatureScale.Celsius, "5.")]
    [InlineData(TemperatureScale.Celsius, "+5")]
    public void Validate_WhenValueIsAtOrAboveMinimum_ReturnsValid(TemperatureScale scale, string text)
    {
        TemperatureValidationOutcome actual = TemperatureValidator.Validate(text, scale);

        Assert.Equal(TemperatureValidationOutcome.Valid, actual);
    }
}
