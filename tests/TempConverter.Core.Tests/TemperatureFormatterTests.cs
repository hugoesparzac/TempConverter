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
}
