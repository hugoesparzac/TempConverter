using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;

namespace TempConverter.App.Tests;

public class MainWindowConverterTests
{
    [AvaloniaFact]
    public void MainWindow_WhenCelsiusTyped_FahrenheitResultAppearsWithoutButton()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        TextBox? celsiusInput = window.FindControl<TextBox>("CelsiusInput");
        Assert.NotNull(celsiusInput);

        celsiusInput.Focus();
        celsiusInput.Text = "37";

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "FahrenheitResultText");
        Assert.Equal("98.6", resultBlock.Text);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_ShowsCelsiusAndFahrenheitLabels()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        var texts = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Select(block => block.Text)
            .ToList();
        Assert.Contains("°C", texts);
        Assert.Contains("°F", texts);

        window.Close();
    }
}
