using System.Globalization;

namespace TempConverter.Core.Tests;

public class ConversionHistoryTests
{
    [Fact]
    public void Add_PutsNewestEntryFirst()
    {
        ConversionHistory history = new();
        ConversionHistoryEntry celsiusToFahrenheit = new("37", TemperatureScale.Celsius, "98.6", TemperatureScale.Fahrenheit);
        ConversionHistoryEntry celsiusToKelvin = new("37", TemperatureScale.Celsius, "310.15", TemperatureScale.Kelvin);

        history.Add(celsiusToFahrenheit);
        history.Add(celsiusToKelvin);

        Assert.Equal([celsiusToKelvin, celsiusToFahrenheit], history.Entries);
    }

    [Fact]
    public void Add_DropsOldestWhenExceedingTwentyEntries()
    {
        ConversionHistory history = new();
        List<ConversionHistoryEntry> added = [];
        for (int degree = 1; degree <= 21; degree++)
        {
            added.Add(new ConversionHistoryEntry(
                degree.ToString(CultureInfo.InvariantCulture),
                TemperatureScale.Celsius,
                "0",
                TemperatureScale.Kelvin));
        }

        foreach (ConversionHistoryEntry entry in added)
        {
            history.Add(entry);
        }

        Assert.Equal(20, history.Entries.Count);
        Assert.Equal([.. added.Skip(1).Reverse()], history.Entries);
        Assert.DoesNotContain(added[0], history.Entries);
    }

    [Fact]
    public void Clear_RemovesAllEntries()
    {
        ConversionHistory history = new();
        history.Add(new ConversionHistoryEntry("37", TemperatureScale.Celsius, "98.6", TemperatureScale.Fahrenheit));
        history.Add(new ConversionHistoryEntry("0", TemperatureScale.Celsius, "273.15", TemperatureScale.Kelvin));

        history.Clear();

        Assert.Empty(history.Entries);
    }

    [Theory]
    [InlineData("37", TemperatureScale.Celsius, "98.6", TemperatureScale.Fahrenheit, "37 °C → 98.6 °F")]
    [InlineData("37", TemperatureScale.Celsius, "310.15", TemperatureScale.Kelvin, "37 °C → 310.15 K")]
    [InlineData("-40", TemperatureScale.Fahrenheit, "-40", TemperatureScale.Celsius, "-40 °F → -40 °C")]
    public void Entry_Display_ShowsValueWithSourceScaleAndResultWithTargetScale(
        string valueText,
        TemperatureScale sourceScale,
        string resultText,
        TemperatureScale targetScale,
        string expected)
    {
        ConversionHistoryEntry entry = new(valueText, sourceScale, resultText, targetScale);

        Assert.Equal(expected, entry.Display);
    }
}
