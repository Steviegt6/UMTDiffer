using System;
using System.Globalization;
using System.Windows.Data;

namespace UndertaleModTool;

[ValueConversion(typeof(UmtTheme), typeof(bool))]
internal sealed class UmtThemeIsEqualsConverter : IMultiValueConverter
{
    public object? Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null || value.Length != 2)
        {
            return false;
        }

        if (value[0] is not UmtTheme source || value[1] is not UmtTheme target)
        {
            return false;
        }

        return StringComparer.Ordinal.Equals(source.Name, target.Name);
    }

    public object[] ConvertBack(object? value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}