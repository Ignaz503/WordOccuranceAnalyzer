namespace TextFileContentAnalyzer.Core.Mediator;
/// <summary>
/// Asynchrounous mediator to allow for object communication without tight coupling
/// </summary>
/// <typeparam name="T">Type of messages being sent</typeparam>
public interface IAsyncMediator<T> : IAsyncPublisher<T>
{
    void Subsrcibe(AsyncMessageHandler<T> handler);
    bool Unsubscribe(AsyncMessageHandler<T> handler);
    
    public bool IsBusy { get; }

}


