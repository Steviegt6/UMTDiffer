using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace UndertaleModTool.Aak.Commands;

/// <summary>
///     Wrapper around <see cref="ICommand"/> that allows for easy declaration
///     of commands through delegates.
/// </summary>
internal sealed class RelayCommand<T> : ICommand
{
    private readonly Action<T?>     execute;
    private readonly Predicate<T?>? canExecute;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action<T?> execute)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public RelayCommand(Action<T?> execute, Predicate<T?> canExecute)
    {
        this.execute    = execute    ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanExecute(object? parameter)
    {
        return canExecute?.Invoke((T?)parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        execute((T?)parameter);
    }
}