using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelSwapTests
{
    [Theory]
    [InlineData("100", TemperatureScale.Celsius, TemperatureScale.Fahrenheit, "212", TemperatureScale.Fahrenheit, TemperatureScale.Celsius, "100")]
    [InlineData("0", TemperatureScale.Celsius, TemperatureScale.Kelvin, "273.15", TemperatureScale.Kelvin, TemperatureScale.Celsius, "0")]
    public void SwapCommand_ValidResult_ExchangesScalesAndCarriesResultIntoInput(
        string initialInput,
        TemperatureScale initialSourceScale,
        TemperatureScale initialTargetScale,
        string expectedCarriedInput,
        TemperatureScale expectedSourceScale,
        TemperatureScale expectedTargetScale,
        string expectedRecalculatedResult)
    {
        var viewModel = new MainViewModel
        {
            Input = initialInput,
            SourceScale = initialSourceScale,
            TargetScale = initialTargetScale,
        };

        List<string?> notified = [];
        viewModel.PropertyChanged += (_, e) => notified.Add(e.PropertyName);

        viewModel.SwapScalesCommand.Execute(null);

        Assert.Equal(expectedSourceScale, viewModel.SourceScale);
        Assert.Equal(expectedTargetScale, viewModel.TargetScale);
        Assert.Equal(expectedCarriedInput, viewModel.Input);
        Assert.Equal(expectedRecalculatedResult, viewModel.Result);
        Assert.Contains(nameof(MainViewModel.Result), notified);
    }

    [Fact]
    public void SwapCommand_Twice_RestoresOriginalScalesAndValue()
    {
        var viewModel = new MainViewModel
        {
            Input = "100",
            SourceScale = TemperatureScale.Celsius,
            TargetScale = TemperatureScale.Fahrenheit,
        };

        viewModel.SwapScalesCommand.Execute(null);
        Assert.Equal("212", viewModel.Input);
        Assert.Equal("100", viewModel.Result);

        viewModel.SwapScalesCommand.Execute(null);
        Assert.Equal(TemperatureScale.Celsius, viewModel.SourceScale);
        Assert.Equal(TemperatureScale.Fahrenheit, viewModel.TargetScale);
        Assert.Equal("100", viewModel.Input);
        Assert.Equal("212", viewModel.Result);
    }

    [Theory]
    [InlineData("", TemperatureScale.Celsius, TemperatureScale.Fahrenheit, "", "")]
    [InlineData("abc", TemperatureScale.Celsius, TemperatureScale.Kelvin, "abc", "")]
    [InlineData("-300", TemperatureScale.Celsius, TemperatureScale.Fahrenheit, "-300", "-184.44")]
    public void SwapCommand_WithoutValidResult_ExchangesScalesAndPreservesInput(
        string input,
        TemperatureScale initialSourceScale,
        TemperatureScale initialTargetScale,
        string expectedPreservedInput,
        string expectedRecalculatedResult)
    {
        var viewModel = new MainViewModel
        {
            Input = input,
            SourceScale = initialSourceScale,
            TargetScale = initialTargetScale,
        };
        Assert.Equal(string.Empty, viewModel.Result);

        viewModel.SwapScalesCommand.Execute(null);

        Assert.Equal(initialTargetScale, viewModel.SourceScale);
        Assert.Equal(initialSourceScale, viewModel.TargetScale);
        Assert.Equal(expectedPreservedInput, viewModel.Input);
        Assert.Equal(expectedRecalculatedResult, viewModel.Result);
    }
}
