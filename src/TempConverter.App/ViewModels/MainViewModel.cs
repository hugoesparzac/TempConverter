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
        TemperatureValidator.TryParseValue(Input, out double value)
            && TemperatureValidator.Validate(value, SourceScale) == TemperatureValidationOutcome.Valid
            ? TemperatureFormatter.Format(TemperatureConverter.Convert(value, SourceScale, TargetScale))
            : string.Empty;

    public string ErrorMessage =>
        string.IsNullOrEmpty(Input)
            ? string.Empty
            : TemperatureValidator.Validate(Input, SourceScale) switch
            {
                TemperatureValidationOutcome.Valid => string.Empty,
                TemperatureValidationOutcome.NotANumber => "Enter a valid number.",
                TemperatureValidationOutcome.BelowAbsoluteZero =>
                    $"Below absolute zero (minimum {TemperatureFormatter.Format(SourceScale.GetAbsoluteZeroMinimum())} {SourceScale.GetSymbol()}).",
                _ => string.Empty,
            };

    partial void OnInputChanged(string value)
    {
        OnPropertyChanged(nameof(Result));
        OnPropertyChanged(nameof(ErrorMessage));
    }

    partial void OnSourceScaleChanged(TemperatureScale value)
    {
        OnPropertyChanged(nameof(Result));
        OnPropertyChanged(nameof(ErrorMessage));
    }

    partial void OnTargetScaleChanged(TemperatureScale value) => OnPropertyChanged(nameof(Result));
}
