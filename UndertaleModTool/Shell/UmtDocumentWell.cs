using System.Windows.Input;

using UndertaleModTool.Commands;

namespace UndertaleModTool.Shell;

internal abstract class UmtDocumentWell : UmtViewElement
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

    public  ICommand? activeCommand;
    public  ICommand? closeCommand;
    private string?   toolTip;
}