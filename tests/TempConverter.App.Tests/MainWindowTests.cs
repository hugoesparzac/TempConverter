using Avalonia.Headless.XUnit;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;

namespace TempConverter.App.Tests;

public class MainWindowTests
{
    [AvaloniaFact]
    public void MainWindow_WhenShownWithMainViewModel_RendersAndBindsDataContext()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        Assert.True(window.IsVisible);
        Assert.Same(viewModel, window.DataContext);

        window.Close();
    }
}
