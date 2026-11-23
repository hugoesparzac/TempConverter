using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using TempConverter.Core;

namespace TempConverter.App.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string AppName { get; set; } = "TempConverter";

    [ObservableProperty]
    public partial string Input { get; set; } = string.Empty;

    public string Result =>
        double.TryParse(Input.Replace(',', '.'), CultureInfo.InvariantCulture, out double celsius)
            ? TemperatureFormatter.Format(CelsiusConversions.ToFahrenheit(celsius))
            : string.Empty;

    partial void OnInputChanged(string value) => OnPropertyChanged(nameof(Result));
}
