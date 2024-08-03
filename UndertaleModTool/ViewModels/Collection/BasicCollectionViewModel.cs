using System.Collections.ObjectModel;

using UndertaleModTool.Shell;

namespace UndertaleModTool.ViewModels.Collection;

internal sealed class BasicCollectionViewModel : UmtCollectionViewModel
{
    public BasicCollectionViewModel(StyleSelectorViewModel parent) : base(parent, "WPF Basic Control Styles", "Basic", true)
    {
        Items = new ObservableCollection<UmtDocumentWell>()
        {
            new UmtDocumentWellViewModel(new ButtonView(), "Button", this),
        };
    }
}