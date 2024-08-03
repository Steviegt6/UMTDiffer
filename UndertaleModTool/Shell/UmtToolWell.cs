namespace UndertaleModTool.Shell;

internal abstract class UmtToolWell : UmtViewElement
{
    public bool CanHide
    {
        get => canHide;
        set => SetProperty(ref canHide, value);
    }

    public bool IsVisible
    {
        get => isVisible;
        set => SetProperty(ref isVisible, value);
    }

    private bool canHide   = true;
    private bool isVisible = true;
}