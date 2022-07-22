using TextFileContentAnalyzer.Core.ServiceProvider;

namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// Simple Mediator to publish and listen to messages
/// </summary>
/// <typeparam name="T">The published message type</typeparam>
public class Publisher<T> : IMediator<T>
{
    public SubscriptionMode Mode { get; init; } = SubscriptionMode.DuplicatesAllowed;
    public bool IsBusy { get; private set; }

    readonly object _lockObj = new();

    readonly List<MessageHandler<T>> handlers;

    public int SubscribedHandlerCount => handlers.Count;

    [ServiceCtor]
    public Publisher()
     => handlers = new();

    public Publisher(int capacity)
    => handlers = new(capacity);

    public Publisher(IEnumerable<MessageHandler<T>> collection)
    => handlers = new(collection);

    public void Publish(T message)
    {
        lock (_lockObj) 
        {
            IsBusy = true;
            foreach(var handler in handlers)
                handler.Invoke(message);
            IsBusy = false;
        }
    }

    public void Subsrcibe(MessageHandler<T> handler)
    {
        if(Mode == SubscriptionMode.NoDuplicatesAllowewd && handlers.Contains(handler))
            return;
        handlers.Add(handler);
    }

    public bool Unsubscribe(MessageHandler<T> handler)
    {
        if (IsBusy)
            return false;
        return handlers.Remove(handler);
    }

}


