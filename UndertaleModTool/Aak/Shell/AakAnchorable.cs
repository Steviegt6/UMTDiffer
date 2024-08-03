using UndertaleModTool.Aak.Shell;

namespace AakStudio.Shell.UI.Showcase.Shell;

/// <summary>
///     An anchorable, which may be anchored alongside documents. Often in the
///     form of tool windows.
/// </summary>
internal abstract class AakAnchorable : AakViewElement
{
    /// <summary>
    ///     Whether this view can be hidden.
    /// </summary>
    public bool CanHide
    {
        get => canHide;
        set => SetProperty(ref canHide, value);
    }

    /// <summary>
    ///     Whether this view is currently visible.
    /// </summary>
    public bool IsVisible
    {
        get => isVisible;
        set => SetProperty(ref isVisible, value);
    }

    private bool canHide   = true;
    private bool isVisible = true;
}