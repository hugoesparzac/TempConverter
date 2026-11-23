using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelValidationTests
{
    [Fact]
    public void ErrorMessage_WhenInputIsInvalidText_ShowsEnterValidNumberAndClearsResult()
    {
        var viewModel = new MainViewModel
        {
            Input = "abc"
        };

        Assert.Equal("Enter a valid number.", viewModel.ErrorMessage);
        Assert.Equal(string.Empty, viewModel.Result);
    }

    [Fact]
    public void ErrorMessage_WhenBelowAbsoluteZero_StatesBelowZeroWithScaleMinimum()
    {
        var viewModel = new MainViewModel
        {
            Input = "-300"
        };

        Assert.Equal("Below absolute zero (minimum -273.15 °C).", viewModel.ErrorMessage);
        Assert.Equal(string.Empty, viewModel.Result);
    }

    [Fact]
    public void ErrorMessage_WhenSourceScaleChanges_AdaptsMinimumToNewScale()
    {
        var viewModel = new MainViewModel
        {
            Input = "-1"
        };
        Assert.Equal(string.Empty, viewModel.ErrorMessage);

        bool errorMessageNotified = false;
        viewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(MainViewModel.ErrorMessage))
            {
                errorMessageNotified = true;
            }
        };

        viewModel.SourceScale = TemperatureScale.Kelvin;

        Assert.True(errorMessageNotified, "ErrorMessage did not raise PropertyChanged when source scale changed.");
        Assert.Equal("Below absolute zero (minimum 0 K).", viewModel.ErrorMessage);
        Assert.Equal(string.Empty, viewModel.Result);
    }

    [Fact]
    public void ErrorMessage_WhenInputCorrectedToMinimum_ClearsAndShowsLiveResult()
    {
        var viewModel = new MainViewModel
        {
            Input = "-300"
        };
        Assert.Equal("Below absolute zero (minimum -273.15 °C).", viewModel.ErrorMessage);

        viewModel.Input = "-273.15";

        Assert.Equal(string.Empty, viewModel.ErrorMessage);
        Assert.Equal("-459.67", viewModel.Result);
    }
}
