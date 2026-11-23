using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Platform;
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

    [AvaloniaFact]
    public void MainWindow_WhenShown_HasTempConverterTitle()
    {
        var window = new MainWindow();

        window.Show();

        Assert.Equal("TempConverter", window.Title);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenShown_UsesCustomThermometerIcon()
    {
        var window = new MainWindow();

        window.Show();

        Assert.NotNull(window.Icon);

        using Stream assetStream = AssetLoader.Open(new Uri("avares://TempConverter/Assets/app-icon.ico"));
        var expectedIcon = new WindowIcon(assetStream);
        using var expectedBytes = new MemoryStream();
        expectedIcon.Save(expectedBytes);

        using var actualBytes = new MemoryStream();
        window.Icon.Save(actualBytes);

        Assert.Equal(expectedBytes.ToArray(), actualBytes.ToArray());

        window.Close();
    }
}
