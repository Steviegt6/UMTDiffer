using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using UndertaleModTool.Commands;
using UndertaleModTool.Shell;

namespace UndertaleModTool.ViewModels;

internal sealed class WorkSpaceViewModel : ViewModelBase
{
    public static WorkSpaceViewModel Default { get; } = new();

    public ObservableCollection<UmtToolWell> Anchorables
    {
        get => anchorables;
        set => OnPropertyChanged(ref anchorables, value, nameof(Anchorables));
    }

    public ObservableCollection<UmtDocumentWell> DocumentViews
    {
        get => documentViews;
        set => OnPropertyChanged(ref documentViews, value, nameof(DocumentViews));
    }

    public UmtTheme CurrentTheme
    {
        get => currentTheme;

        set
        {
            UmtXamlUIResource.Instance.Theme = value;
            OnPropertyChanged(ref currentTheme, value, nameof(CurrentTheme));
        }
    }

    public UmtViewElement? ActiveDocument
    {
        get => activeDocument;
        set => OnPropertyChanged(ref activeDocument, value, nameof(ActiveDocument));
    }

    public ICommand ThemeSwitchCommand
    {
        get => themeSwitchCommand ??= new RelayCommand<UmtTheme>(OnThemeSwitch);
    }

    private WorkSpaceViewModel()
    {
        StyleSelector = new StyleSelectorViewModel(this);
        currentTheme  = UmtXamlUIResource.Instance.Theme;

        anchorables   = new ObservableCollection<UmtToolWell>() { StyleSelector };
        documentViews = new ObservableCollection<UmtDocumentWell>();
    }

    public StyleSelectorViewModel StyleSelector { get; }

    private ObservableCollection<UmtToolWell>     anchorables;
    private ObservableCollection<UmtDocumentWell> documentViews;
    private UmtTheme                              currentTheme;

    private UmtViewElement? activeDocument;
    private ICommand?       themeSwitchCommand;

    private void OnThemeSwitch(UmtTheme? newTheme)
    {
        if (newTheme is not null)
        {
            CurrentTheme = newTheme;
        }
    }

    public void AddOrActiveDocument(UmtDocumentWell view)
    {
        var item = DocumentViews.FirstOrDefault();
        if (item == null)
        {
            item = view;
            DocumentViews.Add(item);
        }
        ActiveDocument = item;
    }

    public void CloseDocument(UmtDocumentWell view)
    {
        if (DocumentViews.Contains((UmtDocumentWell)view))
        {
            DocumentViews.Remove(view);
            ActiveDocument = DocumentViews.FirstOrDefault();
        }
    }

    public void AddOrActiveAnchor(UmtToolWell view)
    {
        var item = Anchorables.FirstOrDefault(x => x == view);
        if (item == null)
        {
            item = view;
            Anchorables.Add(item);
        }
        ActiveDocument = item;
    }

    public void CloseAnchor(UmtToolWell view)
    {
        if (Anchorables.Contains((UmtToolWell)view))
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