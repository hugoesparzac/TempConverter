namespace TempConverter.Core.Tests;

public class TemperatureFormatterTests
{
    [Theory]
    [InlineData(212.0, "212")]
    [InlineData(98.6, "98.6")]
    [InlineData(97.88, "97.88")]
    [InlineData(97.8, "97.8")]
    [InlineData(97.862, "97.86")]
    [InlineData(97.867, "97.87")]
    public void Format_TwoOrFewerDecimals_TrimsTrailingZeros(double value, string expected)
    {
        string actual = TemperatureFormatter.Format(value);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(98.6, 0, "99")]
    [InlineData(-40.4, 0, "-40")]
    [InlineData(26.85, 0, "27")]
    public void Format_PrecisionZero_RoundsToInteger(double value, int precision, string expected)
    {
        string actual = TemperatureFormatter.Format(value, precision);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2.7777777777777777, 6, "2.777778")]
    [InlineData(2.7777777777777777, 4, "2.7778")]
    [InlineData(310.9277777777778, 6, "310.927778")]
    [InlineData(310.9277777777778, 4, "310.9278")]
    public void Format_PrecisionFourAndSix_RoundsToSelectedDecimals(double value, int precision, string expected)
    {
        string actual = TemperatureFormatter.Format(value, precision);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(212.0, 2, "212")]
    [InlineData(212.0, 4, "212")]
    [InlineData(36.6, 6, "36.6")]
    [InlineData(98.6, 4, "98.6")]
    public void Format_TrailingZerosTrimmedAtAnyPrecision(double value, int precision, string expected)
    {
        string actual = TemperatureFormatter.Format(value, precision);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(123.456, "123.46")]
    [InlineData(5.499999, "5.5")]
    public void Format_WithoutPrecision_KeepsTwoDecimalRule(double value, string expected)
    {
        string actual = TemperatureFormatter.Format(value);

        Assert.Equal(expected, actual);
    }
}
