using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using AakStudio.Shell.UI.Showcase;
using AakStudio.Shell.UI.Showcase.Shell;
using AakStudio.Shell.UI.Showcase.ViewModels;

using UndertaleModTool.Aak.Commands;
using UndertaleModTool.Aak.Shell;
using UndertaleModTool.Aak.ViewModels;

namespace UndertaleModTool.ViewModels;

internal sealed class WorkSpaceViewModel : ViewModelBase
{
    public static WorkSpaceViewModel Default { get; } = new();

    public ObservableCollection<AakAnchorable> Anchorables
    {
        get => anchorables;
        set => OnPropertyChanged(ref anchorables, value, nameof(Anchorables));
    }

    public ObservableCollection<AakDocument> DocumentViews
    {
        get => documentViews;
        set => OnPropertyChanged(ref documentViews, value, nameof(DocumentViews));
    }

    public AakTheme CurrentTheme
    {
        get => currentTheme;

        set
        {
            AakXamlUIResource.Instance.Theme = value;
            OnPropertyChanged(ref currentTheme, value, nameof(CurrentTheme));
        }
    }

    public AakViewElement? ActiveDocument
    {
        get => activeDocument;
        set => OnPropertyChanged(ref activeDocument, value, nameof(ActiveDocument));
    }

    public ICommand ThemeSwitchCommand
    {
        get => themeSwitchCommand ??= new RelayCommand<AakTheme>(OnThemeSwitch);
    }

    private WorkSpaceViewModel()
    {
        ProjectExplorer = new ProjectExplorerViewModel(this);
        currentTheme    = AakXamlUIResource.Instance.Theme;

        anchorables   = new ObservableCollection<AakAnchorable>() { ProjectExplorer };
        documentViews = new ObservableCollection<AakDocument>();
    }

    public ProjectExplorerViewModel ProjectExplorer { get; }

    private ObservableCollection<AakAnchorable>     anchorables;
    private ObservableCollection<AakDocument> documentViews;
    private AakTheme                              currentTheme;

    private AakViewElement? activeDocument;
    private ICommand?       themeSwitchCommand;

    private void OnThemeSwitch(AakTheme? newTheme)
    {
        if (newTheme is not null)
        {
            CurrentTheme = newTheme;
        }
    }

    public void AddOrActiveDocument(AakDocument view)
    {
        var item = DocumentViews.FirstOrDefault(x => x == view);
        if (item == null)
        {
            item = view;
            DocumentViews.Add(item);
        }
        ActiveDocument = item;
    }

    public void CloseDocument(AakDocument view)
    {
        if (DocumentViews.Contains(view))
        {
            DocumentViews.Remove(view);
            ActiveDocument = DocumentViews.FirstOrDefault();
        }
    }

    public void AddOrActiveAnchor(AakAnchorable view)
    {
        var item = Anchorables.FirstOrDefault(x => x == view);
        if (item == null)
        {
            item = view;
            Anchorables.Add(item);
        }
        ActiveDocument = item;
    }

    public void CloseAnchor(AakAnchorable view)
    {
        if (Anchorables.Contains(view))
        {
            Anchorables.Remove(view);
            if (Anchorables.Count == 0)
            {
                ActiveDocument = DocumentViews.FirstOrDefault();
            }
            else
            {
                ActiveDocument = Anchorables.FirstOrDefault();
            }
        }
    }
}