using System.Collections.ObjectModel;

using AakStudio.Shell.UI.Showcase.Shell;
using AakStudio.Shell.UI.Showcase.ViewModels;
using AakStudio.Shell.UI.Showcase.ViewModels.Collection;

namespace UndertaleModTool.ViewModels.Collection;

internal sealed class DataCollectionViewModel : AakCollectionViewModel
{
    public DataCollectionViewModel(ProjectExplorerViewModel parent) : base(parent, "Welcome to UnderaleModTool!", "Data", true)
    {
        Items = new ObservableCollection<AakDocumentWell>
        {
            
        };
    }
}