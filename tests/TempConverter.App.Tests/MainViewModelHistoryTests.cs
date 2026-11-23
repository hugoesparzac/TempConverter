using System.Globalization;
using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelHistoryTests
{
    [Fact]
    public void CommitConversion_WithValidInput_AddsEntryToTopOfHistory()
    {
        var viewModel = new MainViewModel
        {
            Input = "37"
        };

        viewModel.CommitConversionCommand.Execute(null);

        Assert.Single(viewModel.History);
        Assert.Equal("37 °C → 98.6 °F", viewModel.History[0].Display);

        viewModel.TargetScale = TemperatureScale.Kelvin;
        viewModel.CommitConversionCommand.Execute(null);

        Assert.Equal(2, viewModel.History.Count);
        Assert.Equal("37 °C → 310.15 K", viewModel.History[0].Display);
        Assert.Equal("37 °C → 98.6 °F", viewModel.History[1].Display);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("-500")]
    public void CommitConversion_WithInvalidInput_DoesNotRecord(string input)
    {
        var viewModel = new MainViewModel
        {
            Input = input
        };

        viewModel.CommitConversionCommand.Execute(null);

        Assert.Empty(viewModel.History);
    }

    [Fact]
    public void CommitConversion_CapturesPrecisionInEffectAtCommit()
    {
        var viewModel = new MainViewModel
        {
            Input = "0",
            TargetScale = TemperatureScale.Kelvin,
            Precision = 2
        };

        viewModel.CommitConversionCommand.Execute(null);

        Assert.Equal("0 °C → 273.15 K", viewModel.History[0].Display);

        viewModel.Precision = 0;
        Assert.Equal("0 °C → 273.15 K", viewModel.History[0].Display);

        viewModel.CommitConversionCommand.Execute(null);

        Assert.Equal("0 °C → 273 K", viewModel.History[0].Display);
        Assert.Equal("0 °C → 273.15 K", viewModel.History[1].Display);
    }

    [Fact]
    public void CommitConversion_Repeated21Times_KeepsOnlyNewestTwenty()
    {
        var viewModel = new MainViewModel();

        for (int degree = 1; degree <= 21; degree++)
        {
            viewModel.Input = degree.ToString(CultureInfo.InvariantCulture);
            viewModel.CommitConversionCommand.Execute(null);
        }

        Assert.Equal(20, viewModel.History.Count);
        Assert.Equal("21", viewModel.History[0].ValueText);
        Assert.Equal("2", viewModel.History[19].ValueText);
        Assert.DoesNotContain(viewModel.History, entry => entry.ValueText == "1");
    }

    [Fact]
    public void ClearHistory_RemovesAllEntries()
    {
        var viewModel = new MainViewModel
        {
            Input = "37"
        };
        viewModel.CommitConversionCommand.Execute(null);
        viewModel.CommitConversionCommand.Execute(null);

        viewModel.ClearHistoryCommand.Execute(null);

        Assert.Empty(viewModel.History);
    }

    [Fact]
    public void NewViewModel_HasEmptyHistory()
    {
        var viewModel = new MainViewModel();

        Assert.Empty(viewModel.History);
    }
}
