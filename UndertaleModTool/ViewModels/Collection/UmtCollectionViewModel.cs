using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using UndertaleModTool.Commands;
using UndertaleModTool.Shell;

namespace UndertaleModTool.ViewModels.Collection;

internal abstract class UmtCollectionViewModel : UmtCollection
{
    public StyleSelectorViewModel Parent { get; }

    public override IEnumerable<UIElement>? Views
    {
        get
        {
            if (Items is null)
            {
                yield break;
            }

            foreach (var item in Items)
            {
                Label linkLabel = new();
                Hyperlink hyperlink = new(new Run(item.Title))
                {
                    Command = new RelayCommand(
                        () =>
                        {
                            item.IsSelected = true;
                            ActiveDocument(item);
                        }
                    )
                };
                linkLabel.Content = hyperlink;
                yield return linkLabel;
            }
        }
    }

    public UmtCollectionViewModel(StyleSelectorViewModel parent, string title, string displayName, bool isExpanded = false)
    {
        Parent = parent;

        Title       = title;
        IsExpanded  = isExpanded;
        DisplayName = displayName;
    }

    private bool isSubItemActive;

    protected override void OnActive()
    {
        if (isSubItemActive)
        {
            isSubItemActive = false;
            return;
        }
        Parent.ActiveDocument(this);
    }

    protected override void OnClose()
    {
        Parent.CloseTab(this);
    }

    internal void ActiveDocument(UmtDocumentWell view)
    {
        isSubItemActive = true;
        Parent.ActiveDocument(view);
    }

    internal void CloseTab(UmtDocumentWell view)
    {
        Parent.CloseTab(view);
    }
}