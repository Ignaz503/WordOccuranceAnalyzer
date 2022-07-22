using System;

namespace TextFileContentAnalyzer.GUI.Commands;

/// <summary>
/// Relay Command that executes an action given
/// </summary>
public class RelayCommand : CommandBase
{
    public readonly Action<object?> _toExecute;

    public RelayCommand(Action<object?> toExecute, Func<object?, bool> canExecute)
        :base(canExecute)
    {
        _toExecute = toExecute;
    }

    public override void Dispose()
    {}

    public override void Execute(object? parameter)
    {
        IsBusy = true;
        _toExecute(parameter);
        IsBusy = false;
    }
}
