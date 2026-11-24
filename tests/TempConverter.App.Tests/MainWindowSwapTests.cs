using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainWindowSwapTests
{
    [AvaloniaFact]
    public void MainWindow_SwapButton_SitsBetweenScaleSelectors()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        Button? swapButton = window.FindControl<Button>("SwapScalesButton");
        ComboBox? sourceSelector = window.FindControl<ComboBox>("SourceScaleSelector");
        ComboBox? targetSelector = window.FindControl<ComboBox>("TargetScaleSelector");
        Assert.NotNull(swapButton);
        Assert.NotNull(sourceSelector);
        Assert.NotNull(targetSelector);

        int sourceRow = Grid.GetRow(sourceSelector);
        int targetRow = Grid.GetRow(targetSelector);
        Assert.Equal(sourceRow + 1, Grid.GetRow(swapButton));
        Assert.Equal(targetRow - 1, Grid.GetRow(swapButton));
        Assert.Equal(Grid.GetColumn(sourceSelector), Grid.GetColumn(swapButton));
        Assert.Equal(Grid.GetColumn(targetSelector), Grid.GetColumn(swapButton));

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_SwapButton_IsSingleKeyboardFocusableControlBoundToSwapCommand()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();
        window.UpdateLayout();

        Button? swapButton = window.FindControl<Button>("SwapScalesButton");
        Assert.NotNull(swapButton);
        Assert.True(swapButton.IsVisible);
        Assert.True(swapButton.IsEnabled);
        Assert.True(swapButton.Focusable, "Swap button cannot receive keyboard focus.");
        Assert.True(swapButton.IsTabStop, "Swap button is skipped by Tab navigation.");
        Assert.Same(viewModel.SwapScalesCommand, swapButton.Command);

        Button[] boundToSwapCommand = window.GetVisualDescendants()
            .OfType<Button>()
            .Where(button => ReferenceEquals(button.Command, viewModel.SwapScalesCommand))
            .ToArray();
        Assert.Single(boundToSwapCommand);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_SwapButtonCommand_ExchangesSelectorsAndCarriesResultIntoInput()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        Button? swapButton = window.FindControl<Button>("SwapScalesButton");
        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        ComboBox? sourceSelector = window.FindControl<ComboBox>("SourceScaleSelector");
        ComboBox? targetSelector = window.FindControl<ComboBox>("TargetScaleSelector");
        TextBlock? resultText = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.NotNull(swapButton);
        Assert.NotNull(valueInput);
        Assert.NotNull(sourceSelector);
        Assert.NotNull(targetSelector);
        Assert.NotNull(resultText);

        valueInput.Text = "100";
        window.UpdateLayout();
        Assert.Equal("212", resultText.Text);

        swapButton.Command!.Execute(null);
        window.UpdateLayout();

        Assert.Equal(TemperatureScale.Fahrenheit, sourceSelector.SelectedItem);
        Assert.Equal(TemperatureScale.Celsius, targetSelector.SelectedItem);
        Assert.Equal("212", valueInput.Text);
        Assert.Equal("100", resultText.Text);

        window.Close();
    }
}
