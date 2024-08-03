using System.Windows;

using AakStudio.Shell.UI.Showcase.Shell;

using UndertaleModTool.ViewModels;

namespace UndertaleModTool.Aak.ViewModels.Collection;

internal class AakDocumentViewModel : AakDocument
{
    private readonly WorkSpaceViewModel parent;
    
    public AakDocumentViewModel(UIElement view, string title, WorkSpaceViewModel parent)
    {
        this.parent = parent;
        Title  = title;
        View   = view;
    }

    protected override void OnActive()
    {
        parent.AddOrActiveDocument(this);
    }

    protected override void OnClose()
    {
        parent.CloseDocument(this);
    }
}