using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainWindowConverterTests
{
    [AvaloniaFact]
    public void MainWindow_WhenValueTyped_ResultAppearsWithoutButton()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        valueInput.Focus();
        valueInput.Text = "37";

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("98.6", resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_HasScaleSelectorsWithDefaultSelections()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        ComboBox? sourceSelector = window.FindControl<ComboBox>("SourceScaleSelector");
        ComboBox? targetSelector = window.FindControl<ComboBox>("TargetScaleSelector");
        Assert.NotNull(sourceSelector);
        Assert.NotNull(targetSelector);

        Assert.Equal<IEnumerable<object?>>(
            [
                TemperatureScale.Celsius,
                TemperatureScale.Fahrenheit,
                TemperatureScale.Kelvin,
                TemperatureScale.Rankine,
            ],
            sourceSelector.Items);
        Assert.Equal(TemperatureScale.Celsius, sourceSelector.SelectedItem);
        Assert.Equal(TemperatureScale.Fahrenheit, targetSelector.SelectedItem);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_SelectorsShowScaleSymbols()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        ComboBox? sourceSelector = window.FindControl<ComboBox>("SourceScaleSelector");
        ComboBox? targetSelector = window.FindControl<ComboBox>("TargetScaleSelector");
        Assert.NotNull(sourceSelector);
        Assert.NotNull(targetSelector);

        foreach (TemperatureScale scale in Enum.GetValues<TemperatureScale>())
        {
            sourceSelector.SelectedItem = scale;
            targetSelector.SelectedItem = scale;
            window.UpdateLayout();

            Assert.Contains(scale.GetSymbol(), VisibleTexts(sourceSelector));
            Assert.Contains(scale.GetSymbol(), VisibleTexts(targetSelector));
        }

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WithKelvinSelected_TypedCelsiusValue_ShowsKelvinResultLive()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        ComboBox? targetSelector = window.FindControl<ComboBox>("TargetScaleSelector");
        Assert.NotNull(valueInput);
        Assert.NotNull(targetSelector);

        valueInput.Text = "100";
        targetSelector.SelectedItem = TemperatureScale.Kelvin;
        window.UpdateLayout();

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("373.15", resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_ChangingScaleSelector_UpdatesResultWithoutRetyping()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        ComboBox? sourceSelector = window.FindControl<ComboBox>("SourceScaleSelector");
        Assert.NotNull(valueInput);
        Assert.NotNull(sourceSelector);

        valueInput.Text = "100";
        window.UpdateLayout();

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("212", resultBlock.Text);

        sourceSelector.SelectedItem = TemperatureScale.Kelvin;
        window.UpdateLayout();

        Assert.Equal("-279.67", resultBlock.Text);

        window.Close();
    }

    private static List<string> VisibleTexts(ComboBox comboBox) =>
        comboBox.GetVisualDescendants()
            .OfType<TextBlock>()
            .Select(block => block.Text ?? string.Empty)
            .ToList();
}
