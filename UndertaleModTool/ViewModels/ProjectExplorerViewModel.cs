using System.Collections.ObjectModel;

using AakStudio.Shell.UI.Showcase.Shell;
using AakStudio.Shell.UI.Showcase.ViewModels.Collection;
using AakStudio.Shell.UI.Showcase.Views;

using UndertaleModTool.ViewModels.Collection;

namespace UndertaleModTool.ViewModels
{
    internal sealed class ProjectExplorerViewModel : AakToolWell
    {
        public ObservableCollection<AakCollectionViewModel> Collections
        {
            get => collections;
            set => SetProperty(ref collections, value);
        }

        public ProjectExplorerViewModel(WorkSpaceViewModel workSpaceViewModel)
        {
            this.workSpaceViewModel = workSpaceViewModel;
            collections = new ObservableCollection<AakCollectionViewModel>
            {
                new DataCollectionViewModel(this),
            };

            Title = "Project Explorer";
            View  = new ProjectExplorerView { DataContext = this };
        }

        private readonly WorkSpaceViewModel                           workSpaceViewModel;
        private          ObservableCollection<AakCollectionViewModel> collections;

        internal void ActiveDocument(AakDocumentWell view)
        {
            workSpaceViewModel.AddOrActiveDocument(view);
        }

        internal void CloseTab(AakDocumentWell view)
        {
            workSpaceViewModel.CloseDocument(view);
        }
    }
}