using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using UndertaleModTool.Aak.ViewModels.Collection;

namespace UndertaleModTool.ViewModels.Solution;

internal class ItemViewModel : AakDocumentViewModel, INotifyPropertyChanged
{
    public string Name
    {
        get => name;
        set => SetField(ref name, value);
    }

    public ObservableCollection<ItemViewModel> Children
    {
        get => children;
        set => SetField(ref children, value);
    }

    public bool IsFolder => Children.Count > 0;

    public event PropertyChangedEventHandler PropertyChanged;

    private ObservableCollection<ItemViewModel> children = new();
    private string                              name;

    public ItemViewModel(string name, UIElement view, string title, WorkSpaceViewModel parent) : base(view, title, parent)
    {
        Title = this.name = name;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}