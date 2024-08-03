using System;
using System.Windows.Data;

using UndertaleModTool.ViewModels.Collection;

namespace UndertaleModTool;

internal sealed class UmtActiveDocumentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is UmtDocumentWellViewModel or UmtCollectionViewModel ? value : Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is UmtDocumentWellViewModel or UmtCollectionViewModel ? value : Binding.DoNothing;
    }
}