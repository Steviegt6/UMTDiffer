using UndertaleModTool.Aak.Shell;

namespace AakStudio.Shell.UI.Showcase.Shell;

/// <summary>
///     A tool well, also known as a tool window, which is a hide-able view
///     element that may be docked on the sides of the document views.
/// </summary>
internal abstract class AakToolWell : AakViewElement
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