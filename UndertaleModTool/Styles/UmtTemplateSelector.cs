using System.Windows;
using System.Windows.Controls;

using UndertaleModTool.Shell;

namespace UndertaleModTool.Styles;

internal sealed class UmtTemplateSelector : DataTemplateSelector
{
    public DataTemplate? UmtDocumentWellTemplate { get; set; }

    public DataTemplate? UmtToolWellTemplate { get; set; }

    public DataTemplate? UmtCollectionTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is UmtCollection)
            return UmtCollectionTemplate;

        if (item is UmtDocumentWell)
            return UmtDocumentWellTemplate;

        if (item is UmtToolWell)
            return UmtToolWellTemplate;

        return base.SelectTemplate(item, container);
    }
}