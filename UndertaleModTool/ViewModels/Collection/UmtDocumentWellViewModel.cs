using System.Windows;

using UndertaleModTool.Shell;

namespace UndertaleModTool.ViewModels.Collection;

internal sealed class UmtDocumentWellViewModel : UmtDocumentWell
{
    public UmtCollectionViewModel Parent { get; }

    public UmtDocumentWellViewModel(UIElement view, string title, UmtCollectionViewModel parent)
    {
        Parent = parent;
        Title  = title;
        View   = view;
    }

    protected override void OnActive()
    {
        Parent.ActiveDocument(this);
    }

    protected override void OnClose()
    {
        Parent.CloseTab(this);
    }
}