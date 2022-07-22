using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.GUI.Commands;

/// <summary>
/// Represents command that will be executed asynchrounously
/// </summary>
public class AsyncCommand : CommandBase
{
    protected CancellationTokenSource ctSource;
    protected readonly Func<AsyncCommandExecutionContext, Task> _toExecute;
    protected readonly Action<Exception> _exceptionHandler;

    public AsyncCommand(Func<object?, bool> canExecute, Func<AsyncCommandExecutionContext, Task> toExecute, Action<Exception> exceptionHandler)
        : base(canExecute)
    {
        ctSource = new();
        _toExecute = toExecute;
        _exceptionHandler = exceptionHandler;
    }

    public override void Dispose()
    {
        ctSource.Cancel();
        ctSource.Dispose();
    }

    public void Cancel() 
    {
        ctSource.Cancel(true);

        //commands are reused
        ctSource.Dispose();
        ctSource = new();
    }

    public override async void Execute(object? parameter)
    {
        IsBusy = true;
        try
        {
            await _toExecute(new() { Parameter = parameter, Token = ctSource.Token });
        }
        catch (Exception ex)
        {
            _exceptionHandler(ex);
        }
        finally 
        {
            IsBusy = false;
        }
    }

}
