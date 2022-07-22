namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// Publish a message asynchrounously.
/// </summary>
/// <typeparam name="T">Type of the message.</typeparam>
public interface IAsyncPublisher<T> 
{
    Task Publish(T message);
}