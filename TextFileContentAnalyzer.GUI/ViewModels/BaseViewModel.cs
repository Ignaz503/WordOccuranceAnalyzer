using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.GUI.ViewModels;

public abstract class BaseViewModel : IDisposable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public abstract void Dispose();

    protected virtual bool NotifyPropertyChanged<T>(ref T backingfield, T value, [CallerMemberName] string name = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingfield, value))
            return false;
        backingfield = value;
        PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
        return true;
    }

    protected virtual bool NotifyPropertyChanged<T>(ref T backingfield, T value, [CallerMemberName] string name = "", params string[] propagate)
    {
        if (EqualityComparer<T>.Default.Equals(backingfield, value))
            return false;
        backingfield = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        foreach(var prop in propagate)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        return true;
    }

    protected virtual bool NotifyPropertyChanged<T>(ref T backingfield, T value, IEqualityComparer<T> equalityComparer, [CallerMemberName] string name = "")
    {
        if (equalityComparer.Equals(backingfield, value))
            return false;
        backingfield = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        return true;
    }

    protected virtual bool NotifyPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        return true;
    }

    protected virtual bool NotifyPropertyChanged(params string[] names)
    {
        foreach(var name in names)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        return true;
    }
}
