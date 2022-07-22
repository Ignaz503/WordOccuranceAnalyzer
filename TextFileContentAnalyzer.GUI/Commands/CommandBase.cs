using System;
using System.Windows.Input;

namespace TextFileContentAnalyzer.GUI.Commands;


public abstract class CommandBase : ICommand, IDisposable
{
    public readonly Func<object?, bool> _canExecute;

    public bool IsBusy { get; protected set; }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
    public CommandBase(Func<object?, bool> canExecute)
    {
        _canExecute = canExecute;
    }
    public virtual bool CanExecute(object? parameter)
    {
        return _canExecute(parameter);
    }

    public abstract void Execute(object? parameter);

    public abstract void Dispose();
}
