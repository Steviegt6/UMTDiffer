using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UndertaleModTool.Aak.Attach;

/// <summary>
///     Double-click command that handles opening (attaching) documents to the
///     dock.
/// </summary>
internal sealed class MouseLeftDoubleClickAttach
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(MouseLeftDoubleClickAttach),
            new FrameworkPropertyMetadata(OnCommandChanged)
        );

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Control ctrl)
        {
            return;
        }

        ctrl.MouseDoubleClick -= OnMouseDoubleClick;

        if (e.NewValue != null)
        {
            ctrl.MouseDoubleClick += OnMouseDoubleClick;
        }
    }

    private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed || sender is not Control ctrl)
        {
            return;
        }

        var command = GetCommand(ctrl);
        command?.Execute(null);

        e.Handled = true;
    }

    public static ICommand GetCommand(DependencyObject element)
    {
        return (ICommand)element.GetValue(CommandProperty);
    }

    public static void SetCommand(DependencyObject element, bool value)
    {
        element.SetValue(CommandProperty, value);
    }
}