using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

using UndertaleModLib.Models;

namespace UndertaleModTool;

public sealed class MainWindowTitleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
        {
            return "No game loaded";
        }

        var generalInfo = values[0] as UndertaleGeneralInfo;
        var filePath    = values[1] as string;

        if (!string.IsNullOrEmpty(filePath))
        {
            filePath = Path.GetFileName(filePath);
        }

        if (string.IsNullOrEmpty(filePath))
        {
            return generalInfo?.ToString() ?? "No game loaded";
        }

        return string.IsNullOrEmpty(generalInfo?.ToString())
            ? filePath
            : $"{generalInfo} [{filePath}]";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}