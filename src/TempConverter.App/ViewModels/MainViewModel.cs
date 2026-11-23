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

    [ObservableProperty]
    public partial TemperatureScale SourceScale { get; set; } = TemperatureScale.Celsius;

    [ObservableProperty]
    public partial TemperatureScale TargetScale { get; set; } = TemperatureScale.Fahrenheit;

    public IReadOnlyList<TemperatureScale> AvailableScales { get; } = Enum.GetValues<TemperatureScale>();

    public string Result =>
        double.TryParse(Input.Replace(',', '.'), CultureInfo.InvariantCulture, out double value)
            ? TemperatureFormatter.Format(TemperatureConverter.Convert(value, SourceScale, TargetScale))
            : string.Empty;

    partial void OnInputChanged(string value) => OnPropertyChanged(nameof(Result));

    partial void OnSourceScaleChanged(TemperatureScale value) => OnPropertyChanged(nameof(Result));

    partial void OnTargetScaleChanged(TemperatureScale value) => OnPropertyChanged(nameof(Result));
}
