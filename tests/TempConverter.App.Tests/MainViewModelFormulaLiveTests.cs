using TempConverter.App.ViewModels;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainViewModelFormulaLiveTests
{
    [Fact]
    public void Formula_WhenValueOrScaleChanges_UpdatesLiveAndStaysConsistentWithResult()
    {
        var viewModel = new MainViewModel
        {
            Input = "37"
        };
        List<string?> notified = [];
        viewModel.PropertyChanged += (_, e) => notified.Add(e.PropertyName);

        viewModel.Input = "100";
        Assert.Equal("°F = 100 × 9/5 + 32 = 212", viewModel.Formula);
        Assert.Contains(nameof(MainViewModel.Formula), notified);
        Assert.EndsWith("= " + viewModel.Result, viewModel.Formula, StringComparison.Ordinal);

        notified.Clear();
        viewModel.TargetScale = TemperatureScale.Kelvin;
        Assert.Equal("K = 100 + 273.15 = 373.15", viewModel.Formula);
        Assert.Contains(nameof(MainViewModel.Formula), notified);
        Assert.EndsWith("= " + viewModel.Result, viewModel.Formula, StringComparison.Ordinal);

        notified.Clear();
        viewModel.SourceScale = TemperatureScale.Rankine;
        Assert.Equal("K = 100 × 5/9 = 55.56", viewModel.Formula);
        Assert.Contains(nameof(MainViewModel.Formula), notified);
        Assert.EndsWith("= " + viewModel.Result, viewModel.Formula, StringComparison.Ordinal);
    }
}
