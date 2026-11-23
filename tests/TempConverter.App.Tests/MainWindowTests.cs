using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.VisualTree;
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

    [AvaloniaFact]
    public void MainWindow_WhenShown_DisplaysAppNameWithoutTemplateGreeting()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        var texts = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Select(block => block.Text)
            .ToList();

        Assert.Contains("TempConverter", texts);
        Assert.DoesNotContain("Welcome to Avalonia!", texts);

        window.Close();
    }

    [AvaloniaFact]
    public void App_WhenRunning_FollowsSystemThemeVariant()
    {
        var window = new MainWindow();

        window.Show();

        Application app = Application.Current!;
        Assert.Equal(ThemeVariant.Default, app.RequestedThemeVariant);
        Assert.Equal(app.ActualThemeVariant, window.ActualThemeVariant);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenShown_RendersTextWithInterFont()
    {
        var window = new MainWindow();

        window.Show();

        var blocks = window.GetVisualDescendants().OfType<TextBlock>().ToList();
        Assert.NotEmpty(blocks);

        foreach (TextBlock block in blocks)
        {
            Assert.Equal("Inter", block.FontFamily.Name);

            var typeface = new Typeface(block.FontFamily, block.FontStyle, block.FontWeight, block.FontStretch);
            Assert.True(FontManager.Current.TryGetGlyphTypeface(typeface, out GlyphTypeface? glyphTypeface));
            Assert.StartsWith("Inter", glyphTypeface!.FamilyName, StringComparison.Ordinal);
        }

        window.Close();
    }
}
