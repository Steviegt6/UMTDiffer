using System;
using System.Windows.Data;

using AakStudio.Shell.UI.Showcase.ViewModels.Collection;

using UndertaleModTool.Aak.ViewModels.Collection;

namespace UndertaleModTool.Aak.Converters;

/// <summary>
///     Filters out non-document wells.
/// </summary>
internal sealed class AakActiveDocumentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is AakDocumentViewModel or AakCollectionViewModel ? value : Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is AakDocumentViewModel or AakCollectionViewModel ? value : Binding.DoNothing;
    }
}