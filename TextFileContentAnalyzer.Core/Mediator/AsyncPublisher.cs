namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// Simple asynchrounous message mediator
/// </summary>
/// <typeparam name="T">Type of messages being sent</typeparam>
public class AsyncPublisher<T> : IAsyncMediator<T>
{
    public SubscriptionMode Mode { get; init; } = SubscriptionMode.DuplicatesAllowed;

    public bool IsBusy { get; protected set; }

    readonly object _lockObj = new();

    readonly List<AsyncMessageHandler<T>> handlers;

    public AsyncPublisher()
     => handlers = new();

    public AsyncPublisher(int capacity)
    => handlers = new(capacity);

    public AsyncPublisher(IEnumerable<AsyncMessageHandler<T>> collection)
    => handlers = new(collection);

    public async Task Publish(T message)
    {
        IsBusy = true;
        var tasks = new Task[handlers.Count];
        lock (_lockObj) 
        {

            for (int i = 0; i < handlers.Count; i++)
            {
                tasks[i] = handlers[i].Invoke(message);
            }
        }
        IsBusy = false;
        await Task.WhenAll(tasks);
        
    }

    public void Subsrcibe(AsyncMessageHandler<T> handler)
    {
        if (Mode == SubscriptionMode.NoDuplicatesAllowewd && handlers.Contains(handler))
            return;
        handlers.Add(handler);
    }

    public bool Unsubscribe(AsyncMessageHandler<T> handler)
    {
        if (IsBusy)
            return false;
        return handlers.Remove(handler);
    }
}


