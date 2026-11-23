using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelScaleTests
{
    [Fact]
    public void DefaultScales_OnConstruction_AreCelsiusToFahrenheit()
    {
        var viewModel = new MainViewModel();

        Assert.Equal(TemperatureScale.Celsius, viewModel.SourceScale);
        Assert.Equal(TemperatureScale.Fahrenheit, viewModel.TargetScale);
    }

    [Fact]
    public void Result_WhenTargetScaleChanges_RecalculatesWithoutRetyping()
    {
        var viewModel = new MainViewModel
        {
            Input = "100"
        };
        Assert.Equal("212", viewModel.Result);

        bool resultNotified = false;
        viewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(MainViewModel.Result))
            {
                resultNotified = true;
            }
        };

        viewModel.TargetScale = TemperatureScale.Kelvin;

        Assert.True(resultNotified, "Result did not raise PropertyChanged when target scale changed.");
        Assert.Equal("373.15", viewModel.Result);
    }

    [Fact]
    public void Result_WhenSourceScaleChanges_RecalculatesWithoutRetyping()
    {
        var viewModel = new MainViewModel
        {
            Input = "100"
        };
        Assert.Equal("212", viewModel.Result);

        bool resultNotified = false;
        viewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(MainViewModel.Result))
            {
                resultNotified = true;
            }
        };

        viewModel.SourceScale = TemperatureScale.Kelvin;

        Assert.True(resultNotified, "Result did not raise PropertyChanged when source scale changed.");
        Assert.Equal("-279.67", viewModel.Result);
    }
}
