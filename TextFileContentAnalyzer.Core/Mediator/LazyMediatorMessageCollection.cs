
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.Core.Mediator;

public class LazyMediatorMessageCollection<T> : LazyMediatorBase<T> 
{
    readonly BlockingCollection<T> _messages = new();

    public override async Task ProccessMessages()
    {

        var tasks = new Task[_messageHandlers.Count];

        foreach (var message in _messages.GetConsumingEnumerable()) 
        {
            IsBusy = true;
            for (int i = 0; i < _messageHandlers.Count; i++)
            {
                tasks[i] = _messageHandlers[i].Invoke(message);
            }
            IsBusy = false;
            await Task.WhenAll(tasks);
        }
    }

    public override void Publish(T message)
    {
        _messages.Add(message);
    }
}
