using System.Collections.ObjectModel;

using UndertaleModTool.Shell;
using UndertaleModTool.ViewModels.Collection;
using UndertaleModTool.Views;

namespace UndertaleModTool.ViewModels;

internal sealed class StyleSelectorViewModel : UmtToolWell
{
    public ObservableCollection<UmtCollectionViewModel> Collections
    {
        get => collections;
        set => SetProperty(ref collections, value);
    }

    public StyleSelectorViewModel(WorkSpaceViewModel workSpaceViewModel)
    {
        this.workSpaceViewModel = workSpaceViewModel;
        collections = new ObservableCollection<UmtCollectionViewModel>
        {
            new DataCollectionViewModel(this),
        };

        Title = "Wpf Base Styles";
        View  = new StyleSelectorView { DataContext = this };
    }

    private readonly WorkSpaceViewModel                           workSpaceViewModel;
    private          ObservableCollection<UmtCollectionViewModel> collections;


    internal void ActiveDocument(UmtDocumentWell view)
    {
        workSpaceViewModel.AddOrActiveDocument(view);
    }

    internal void CloseTab(UmtDocumentWell view)
    {
        workSpaceViewModel.CloseDocument(view);
    }
}