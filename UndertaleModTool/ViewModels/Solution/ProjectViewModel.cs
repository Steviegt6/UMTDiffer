using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndertaleModTool.ViewModels.Solution;

public class ProjectViewModel : INotifyPropertyChanged
{
    public string Name
    {
        get => name;
        set => SetField(ref name, value);
    }

    public ObservableCollection<ItemViewModel> Items
    {
        get => items;
        set => SetField(ref items, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private ObservableCollection<ItemViewModel> items = new();
    private string                              name;

    public ProjectViewModel(string name)
    {
        this.name = name;
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