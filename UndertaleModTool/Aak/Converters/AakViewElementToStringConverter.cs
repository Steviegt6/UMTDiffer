using System;
using System.Globalization;
using System.Windows.Data;

using AakStudio.Shell.UI.Showcase.Shell;

namespace UndertaleModTool.Aak.Converters;

/// <summary>
///     Fetches a display string (such as the title or name) of an Aak element.
/// </summary>
internal sealed class AakViewElementToStringConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            AakDocument aakDocumentWell => aakDocumentWell.Title,
            _                               => Binding.DoNothing,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}