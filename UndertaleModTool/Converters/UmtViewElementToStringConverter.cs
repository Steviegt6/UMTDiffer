using System;
using System.Globalization;
using System.Windows.Data;

using UndertaleModTool.Shell;

namespace UndertaleModTool;

internal sealed class UmtViewElementToStringConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            UmtCollection umtCollection     => umtCollection.DisplayName,
            UmtDocumentWell umtDocumentWell => umtDocumentWell.Title,
            _                               => Binding.DoNothing,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}