using TempConverter.App.ViewModels;

namespace TempConverter.App.Tests;

public class MainViewModelConversionTests
{
    [Fact]
    public void Result_WhenCelsiusInputChanges_RecalculatesLive()
    {
        var viewModel = new MainViewModel
        {
            Input = "37"
        };

        Assert.Equal("98.6", viewModel.Result);
    }

    [Fact]
    public void Result_WhenInputUsesCommaSeparator_ConvertsCorrectly()
    {
        var viewModel = new MainViewModel
        {
            Input = "36,6"
        };

        Assert.Equal("97.88", viewModel.Result);
    }

    [Fact]
    public void Result_WhenInputEmptyOrInvalid_ClearsResult()
    {
        var viewModel = new MainViewModel
        {
            Input = "abc"
        };
        Assert.Equal(string.Empty, viewModel.Result);

        viewModel.Input = string.Empty;
        Assert.Equal(string.Empty, viewModel.Result);

        viewModel.Input = "37";
        Assert.Equal("98.6", viewModel.Result);
    }

    [Fact]
    public void Result_WhenValueEndsInZeros_FormatsWithoutTrailingZeros()
    {
        var viewModel = new MainViewModel
        {
            Input = "100"
        };

        Assert.Equal("212", viewModel.Result);
    }
}
