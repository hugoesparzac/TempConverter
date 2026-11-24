using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelPrecisionTests
{
    [Fact]
    public void Precision_OnConstruction_IsTwo()
    {
        var viewModel = new MainViewModel();

        Assert.Equal(2, viewModel.Precision);
    }

    [Fact]
    public void Result_WhenPrecisionChanges_ReformatsAndNotifiesWithoutRetyping()
    {
        var viewModel = new MainViewModel
        {
            Input = "37",
            SourceScale = TemperatureScale.Fahrenheit,
            TargetScale = TemperatureScale.Celsius
        };
        List<string?> notified = [];
        viewModel.PropertyChanged += (_, e) => notified.Add(e.PropertyName);

        Assert.Equal("2.78", viewModel.Result);

        viewModel.Precision = 4;
        Assert.Equal("2.7778", viewModel.Result);
        Assert.Contains(nameof(MainViewModel.Result), notified);

        notified.Clear();
        viewModel.Precision = 0;
        Assert.Equal("3", viewModel.Result);
        Assert.Contains(nameof(MainViewModel.Result), notified);
    }

    [Fact]
    public void IncreaseAndDecreasePrecision_ClampAtZeroAndSix()
    {
        var viewModel = new MainViewModel();

        for (int i = 0; i < 8; i++)
        {
            viewModel.IncreasePrecisionCommand.Execute(null);
        }

        Assert.Equal(6, viewModel.Precision);

        for (int i = 0; i < 12; i++)
        {
            viewModel.DecreasePrecisionCommand.Execute(null);
        }

        Assert.Equal(0, viewModel.Precision);
    }

    [Fact]
    public void Input_WhenPrecisionChanges_IsUnchanged()
    {
        var viewModel = new MainViewModel
        {
            Input = "36,60",
            Precision = 4
        };

        viewModel.Precision = 0;

        Assert.Equal("36,60", viewModel.Input);
    }

    [Fact]
    public void Formula_WhenPrecisionChanges_StaysConsistentWithResult()
    {
        var viewModel = new MainViewModel
        {
            Input = "37"
        };
        List<string?> notified = [];
        viewModel.PropertyChanged += (_, e) => notified.Add(e.PropertyName);

        viewModel.Precision = 0;
        Assert.Equal("°F = 37 × 9/5 + 32 = 99", viewModel.Formula);
        Assert.Equal("99", viewModel.Result);
        Assert.Contains(nameof(MainViewModel.Formula), notified);
    }

    [Theory]
    [InlineData(99, 6)]
    [InlineData(-3, 0)]
    public void Precision_SetDirectlyOutOfRange_ClampsToSupportedBounds(int requested, int expected)
    {
        var viewModel = new MainViewModel
        {
            Input = "37",
            Precision = requested
        };

        Assert.Equal(expected, viewModel.Precision);

        viewModel.CommitConversionCommand.Execute(null);

        Assert.Single(viewModel.History);
    }
}
