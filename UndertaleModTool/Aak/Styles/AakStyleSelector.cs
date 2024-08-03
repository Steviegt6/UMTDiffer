using System.Windows;
using System.Windows.Controls;

using AakStudio.Shell.UI.Showcase.Shell;

namespace UndertaleModTool.Aak.Styles;

internal sealed class AakStyleSelector : StyleSelector
{
    public Style? AakDocumentWellStyle { get; set; }

    public Style? AakToolWellStyle { get; set; }

    public override Style? SelectStyle(object item, DependencyObject container)
    {
        return item switch
        {
            AakDocument   => AakDocumentWellStyle,
            AakAnchorable => AakToolWellStyle,
            _             => base.SelectStyle(item, container),
        };
    }
}