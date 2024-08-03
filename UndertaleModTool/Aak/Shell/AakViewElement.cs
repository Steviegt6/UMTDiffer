using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;

namespace UndertaleModTool.Aak.Shell;

/// <summary>
///     A view element, which is the minimum viable object that may be shown in
///     the UI.
/// </summary>
internal abstract class AakViewElement : INotifyPropertyChanged
{
    /// <summary>
    ///     The dockable view to show in the UI.
    /// </summary>
    public UIElement? View
    {
        get => view;
        set => SetProperty(ref view, value);
    }

    /// <summary>
    ///     The title of the view.
    /// </summary>
    /// <remarks>
    ///     This is what goes in the title bar.
    /// </remarks>
    public string? Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    /// <summary>
    ///     Whether this view is active (currently being displayed).
    /// </summary>
    public bool IsActive
    {
        get => isActive;
        set => SetProperty(ref isActive, value);
    }

    /// <summary>
    ///     Whether this view is currently selected.
    /// </summary>
    public bool IsSelected
    {
        get => isSelected;
        set => SetProperty(ref isSelected, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetProperty<T>(
#if NETCOREAPP
        [NotNullIfNotNull(nameof(newValue))]
#endif
        ref T property,
        T                         newValue,
        [CallerMemberName] string propertyName = ""
    )
    {
        property = newValue;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private UIElement? view;
    private string?    title;
    private bool       isActive;
    private bool       isSelected;
}