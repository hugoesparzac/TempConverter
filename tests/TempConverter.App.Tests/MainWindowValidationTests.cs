using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainWindowValidationTests
{
    [AvaloniaFact]
    public void MainWindow_WhenInvalidTextTyped_ShowsInlineMessageAndNoResult()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        valueInput.Text = "abc";
        window.UpdateLayout();

        TextBlock? messageBlock = window.FindControl<TextBlock>("ValidationMessage");
        Assert.NotNull(messageBlock);
        Assert.Equal("Enter a valid number.", messageBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal(string.Empty, resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenBelowAbsoluteZero_ShowsInlineMinimumMessageAndNoResult()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        valueInput.Text = "-300";
        window.UpdateLayout();

        TextBlock? messageBlock = window.FindControl<TextBlock>("ValidationMessage");
        Assert.NotNull(messageBlock);
        Assert.Equal("Below absolute zero (minimum -273.15 °C).", messageBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal(string.Empty, resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenInputCorrectedToLimit_HidesMessageAndShowsResultLive()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        valueInput.Text = "-300";
        window.UpdateLayout();

        TextBlock? messageBlock = window.FindControl<TextBlock>("ValidationMessage");
        Assert.NotNull(messageBlock);
        Assert.Equal("Below absolute zero (minimum -273.15 °C).", messageBlock.Text);

        valueInput.Text = "-273.15";
        window.UpdateLayout();

        Assert.Equal(string.Empty, messageBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("-459.67", resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenSourceScaleChangesWithTypedValue_UpdatesMessageToNewMinimum()
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

        valueInput.Text = "-1";
        window.UpdateLayout();

        TextBlock? messageBlock = window.FindControl<TextBlock>("ValidationMessage");
        Assert.NotNull(messageBlock);
        Assert.Equal(string.Empty, messageBlock.Text);

        sourceSelector.SelectedItem = TemperatureScale.Kelvin;
        window.UpdateLayout();

        Assert.Equal("Below absolute zero (minimum 0 K).", messageBlock.Text);

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal(string.Empty, resultBlock.Text);

        window.Close();
    }
}
