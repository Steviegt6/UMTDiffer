using System;
using System.Windows.Data;

using AakStudio.Shell.UI.Showcase.Shell;

namespace UndertaleModTool.Aak.Converters;

/// <summary>
///     Filters out non-document wells.
/// </summary>
internal sealed class AakActiveDocumentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is AakDocument ? value : Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is AakDocument ? value : Binding.DoNothing;
    }
}