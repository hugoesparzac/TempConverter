using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainWindowFormulaTests
{
    [AvaloniaFact]
    public void MainWindow_WithValidConversion_ShowsFormulaBelowResult()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        valueInput.Text = "37";
        window.UpdateLayout();

        TextBlock? formulaBlock = window.FindControl<TextBlock>("FormulaText");
        Assert.NotNull(formulaBlock);
        Assert.Equal("°F = 37 × 9/5 + 32 = 98.6", formulaBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.EndsWith("= " + resultBlock.Text, formulaBlock.Text, StringComparison.Ordinal);
        Assert.True(
            formulaBlock.Bounds.Y >= resultBlock.Bounds.Bottom,
            "The formula line must render below the result.");

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenTargetScaleSelectorChanges_FormulaUpdatesWithoutRetyping()
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
        window.UpdateLayout();

        TextBlock? formulaBlock = window.FindControl<TextBlock>("FormulaText");
        Assert.NotNull(formulaBlock);
        Assert.Equal("°F = 100 × 9/5 + 32 = 212", formulaBlock.Text);

        targetSelector.SelectedItem = TemperatureScale.Kelvin;
        window.UpdateLayout();

        Assert.Equal("K = 100 + 273.15 = 373.15", formulaBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("373.15", resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenInvalidTextTyped_FormulaAreaIsHidden()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        valueInput.Text = "37";
        window.UpdateLayout();

        TextBlock? formulaBlock = window.FindControl<TextBlock>("FormulaText");
        Assert.NotNull(formulaBlock);
        Assert.Equal("°F = 37 × 9/5 + 32 = 98.6", formulaBlock.Text);

        valueInput.Text = "abc";
        window.UpdateLayout();

        Assert.Equal(string.Empty, formulaBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal(string.Empty, resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenTargetScaleSetToSource_FormulaIsHiddenAndResultShowsValue()
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

        valueInput.Text = "37";
        window.UpdateLayout();

        TextBlock? formulaBlock = window.FindControl<TextBlock>("FormulaText");
        Assert.NotNull(formulaBlock);
        Assert.Equal("°F = 37 × 9/5 + 32 = 98.6", formulaBlock.Text);

        targetSelector.SelectedItem = TemperatureScale.Celsius;
        window.UpdateLayout();

        Assert.Equal(string.Empty, formulaBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("37", resultBlock.Text);

        window.Close();
    }
}
