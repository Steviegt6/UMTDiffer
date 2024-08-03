using System.Collections.ObjectModel;

using UndertaleModTool.Shell;

namespace UndertaleModTool.ViewModels.Collection;

internal sealed class DataCollectionViewModel : UmtCollectionViewModel
{
    public DataCollectionViewModel(StyleSelectorViewModel parent) : base(parent, "Welcome to UndertaleModTool!", "Data", true)
    {
        Items = new ObservableCollection<UmtDocumentWell>()
            { };
    }
}