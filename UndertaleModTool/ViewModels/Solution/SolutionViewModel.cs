using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndertaleModTool.ViewModels.Solution;

public class SolutionViewModel : INotifyPropertyChanged
{
    public string Name
    {
        get => name;
        set => SetField(ref name, value);
    }

    public ObservableCollection<ProjectViewModel> Projects
    {
        get => projects;
        set => SetField(ref projects, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private ObservableCollection<ProjectViewModel> projects = new();
    private string                                 name;

    public SolutionViewModel(string name)
    {
        this.name = name;
    }

    protected virtual void OnPropertyChanged(string propertyName)
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