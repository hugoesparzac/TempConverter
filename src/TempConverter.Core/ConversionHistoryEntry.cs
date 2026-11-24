namespace TempConverter.Core;

public sealed record ConversionHistoryEntry(
    string ValueText,
    TemperatureScale SourceScale,
    string ResultText,
    TemperatureScale TargetScale)
{
    public string Display => $"{ValueText} {SourceScale.GetSymbol()} → {ResultText} {TargetScale.GetSymbol()}";
}
