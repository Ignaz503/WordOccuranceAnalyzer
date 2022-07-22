namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// Publish a message.
/// </summary>
/// <typeparam name="T">Type of message</typeparam>
public interface IPublisher<T> 
{
    void Publish(T message);
}
