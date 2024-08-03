using System.Windows.Input;

using UndertaleModTool.Aak.Commands;
using UndertaleModTool.Aak.Shell;

namespace AakStudio.Shell.UI.Showcase.Shell;

/// <summary>
///     A document well, which is a view element that may 
/// </summary>
internal abstract class AakDocumentWell : AakViewElement
{
    public ICommand ActiveCommand
    {
        get => activeCommand ??= new RelayCommand(OnActive);
    }

    public ICommand CloseCommand
    {
        get => closeCommand ??= new RelayCommand(OnClose);
    }

    public string? ToolTip
    {
        get => toolTip;
        set => SetProperty(ref toolTip, value);
    }

    protected abstract void OnActive();

    protected abstract void OnClose();

    private ICommand? activeCommand;
    private ICommand? closeCommand;
    private string?   toolTip;
}