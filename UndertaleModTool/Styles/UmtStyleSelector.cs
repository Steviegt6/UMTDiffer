using System.Windows;
using System.Windows.Controls;

using UndertaleModTool.Shell;

namespace UndertaleModTool.Styles;

internal sealed class UmtStyleSelector : StyleSelector
{
    public Style? UmtDocumentWellStyle { get; set; }

    public Style? UmtToolWellStyle { get; set; }

    public override Style? SelectStyle(object item, DependencyObject container)
    {
        if (item is UmtDocumentWell)
            return UmtDocumentWellStyle;

        if (item is UmtToolWell)
            return UmtToolWellStyle;

        return base.SelectStyle(item, container);
    }
}