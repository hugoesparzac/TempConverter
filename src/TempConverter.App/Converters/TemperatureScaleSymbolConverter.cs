using System.Globalization;
using Avalonia.Data.Converters;
using TempConverter.Core;

namespace TempConverter.App.Converters;

public sealed class TemperatureScaleSymbolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is TemperatureScale scale ? scale.GetSymbol() : string.Empty;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
