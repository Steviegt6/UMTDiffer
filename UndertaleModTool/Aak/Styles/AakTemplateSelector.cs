using System.Windows;
using System.Windows.Controls;

using AakStudio.Shell.UI.Showcase.Shell;

namespace UndertaleModTool.Aak.Styles;

internal sealed class AakTemplateSelector : DataTemplateSelector
{
    public AakTemplateSelector() { }

    public DataTemplate? AakDocumentWellTemplate { get; set; }

    public DataTemplate? AakToolWellTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        return item switch
        {
            AakDocument   => AakDocumentWellTemplate,
            AakAnchorable => AakToolWellTemplate,
            _             => base.SelectTemplate(item, container),
        };
    }
}