using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelFormulaTests
{
    [Fact]
    public void Formula_WhenValidConversion_ShowsSingleLineWithSubstitutedValue()
    {
        var viewModel = new MainViewModel
        {
            Input = "37"
        };

        Assert.Equal("°F = 37 × 9/5 + 32 = 98.6", viewModel.Formula);
    }

    [Fact]
    public void Formula_WhenInputInvalidOrEmpty_IsEmptyWhileResultAlsoEmpty()
    {
        var viewModel = new MainViewModel
        {
            Input = "abc"
        };
        Assert.Equal(string.Empty, viewModel.Formula);
        Assert.Equal(string.Empty, viewModel.Result);

        viewModel.Input = string.Empty;
        Assert.Equal(string.Empty, viewModel.Formula);

        viewModel.Input = "-300";
        Assert.Equal(string.Empty, viewModel.Formula);
        Assert.Equal(string.Empty, viewModel.Result);

        viewModel.Input = "37";
        Assert.Equal("°F = 37 × 9/5 + 32 = 98.6", viewModel.Formula);
        Assert.Equal("98.6", viewModel.Result);
    }

    [Fact]
    public void Formula_WhenSourceEqualsTarget_IsEmpty()
    {
        var celsiusPair = new MainViewModel
        {
            Input = "37",
            TargetScale = TemperatureScale.Celsius
        };
        Assert.Equal(string.Empty, celsiusPair.Formula);

        var fahrenheitPair = new MainViewModel
        {
            Input = "37",
            SourceScale = TemperatureScale.Fahrenheit,
            TargetScale = TemperatureScale.Fahrenheit
        };
        Assert.Equal(string.Empty, fahrenheitPair.Formula);
        Assert.Equal("37", fahrenheitPair.Result);
    }
}
