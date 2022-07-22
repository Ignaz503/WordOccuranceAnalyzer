using System.Collections.Generic;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.Mediator;

namespace TextFileContentAnalyzer.Core.Mediator;

public abstract class LazyMediatorBase<T> : ILayzyAsyncMediator<T>
{
    protected readonly List<AsyncMessageHandler<T>> _messageHandlers = new();

    protected readonly object _lockObj = new();

    public bool IsBusy { get; protected set; }

    public abstract Task ProccessMessages();
    public abstract void Publish(T message);

    public void Subsrcibe(AsyncMessageHandler<T> handler)
    {
        lock (_lockObj)
        {
            _messageHandlers.Add(handler);
        }
    }

    public bool Unsubscribe(AsyncMessageHandler<T> handler)
    {
        if (IsBusy)
            return false;
        lock (_lockObj)
        {
            return _messageHandlers.Remove(handler);
        }
    }
}
