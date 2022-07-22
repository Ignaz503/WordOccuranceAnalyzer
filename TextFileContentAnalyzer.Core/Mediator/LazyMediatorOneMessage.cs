using System.Threading.Tasks;

namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// La
/// </summary>
/// <typeparam name="T"></typeparam>
public class LazyMediatorOneMessage<T> : LazyMediatorBase<T>
{
    T? _message;

    public override async Task ProccessMessages()
    {
        if (_message is null)
            return;
        var tasks = new Task[_messageHandlers.Count];

        IsBusy = true;
        for (int i = 0; i < _messageHandlers.Count; i++)
        {
            tasks[i] = _messageHandlers[i].Invoke(_message);
        }
        IsBusy = false;
        await Task.WhenAll(tasks);
        
    }

    public override void Publish(T message)
    {
        _message = message;
    }
}