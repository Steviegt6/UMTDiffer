using System.Collections.ObjectModel;
using System.Windows.Controls;

using AakStudio.Shell.UI.Showcase.Shell;
using AakStudio.Shell.UI.Showcase.ViewModels;
using AakStudio.Shell.UI.Showcase.ViewModels.Collection;

namespace UndertaleModTool.ViewModels.Collection;

internal sealed class DataCollectionViewModel : AakCollectionViewModel
{
    private sealed class Folder : AakCollectionViewModel
    {
        private sealed class SubFolder : AakCollectionViewModel
        {
            public SubFolder(ProjectExplorerViewModel parent, string title, string displayName, bool isExpanded = false) : base(parent, title, displayName, isExpanded)
            {
                Items = new ObservableCollection<AakDocumentWell>
                {
                    new AakDocumentWellViewModel(new Button(), "Item 1", this),
                    new AakDocumentWellViewModel(new Button(), "Item 2", this),
                };
            }
        }
        
        public Folder(ProjectExplorerViewModel parent, string title, string displayName, bool isExpanded = false) : base(parent, title, displayName, isExpanded)
        {
            Items = new ObservableCollection<AakDocumentWell>
            {
                new AakDocumentWellViewModel(new Button(), "Item 1", this),
                new AakDocumentWellViewModel(new Button(), "Item 2", this),
                new SubFolder(parent, "Folder 1", "Folder 1", true),
                new SubFolder(parent, "Folder 2", "Folder 2", true),
            };
        }
    }
    
    public DataCollectionViewModel(ProjectExplorerViewModel parent) : base(parent, "Welcome to UnderaleModTool!", "Data", true)
    {
        Items = new ObservableCollection<AakDocumentWell>
        {
            new AakDocumentWellViewModel(new Button(), "Item 1", this),
            new Folder(parent, "Folder 1", "Folder 1", false),
            new Folder(parent, "Folder 2", "Folder 2", true),
        };
    }
}