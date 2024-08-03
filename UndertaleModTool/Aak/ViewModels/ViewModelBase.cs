using System.ComponentModel;

namespace UndertaleModTool.Aak.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanged<T>(ref T property, T newValue, string propertyName)
    {
        property = newValue;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}