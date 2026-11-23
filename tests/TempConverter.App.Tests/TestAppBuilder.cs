using Avalonia;
using Avalonia.Headless;
using TempConverter.App;
using TempConverter.App.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace TempConverter.App.Tests;

public sealed class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .WithInterFont()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
