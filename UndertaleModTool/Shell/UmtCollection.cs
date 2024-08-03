using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace UndertaleModTool.Shell;

internal abstract class UmtCollection : UmtDocumentWell
{
    public bool IsExpanded
    {
        get => isExpanded;
        set => SetProperty(ref isExpanded, value);
    }

    public string? DisplayName
    {
        get => displayName;
        set => SetProperty(ref displayName, value);
    }

    public virtual IEnumerable<UIElement>? Views
    {
        get { yield break; }
    }

    public ObservableCollection<UmtDocumentWell>? Items
    {
        get => items;
        set => SetProperty(ref items, value);
    }

    private ObservableCollection<UmtDocumentWell>? items;
    private string?                                displayName;
    private bool                                   isExpanded;
}