using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.Mediator;

namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// Lazy publishing only forwards messages once Process has been called.
/// </summary>
/// <typeparam name="T">Type of the message.</typeparam>
public interface ILayzyAsyncMediator<T> : IPublisher<T>
{
    Task ProccessMessages();


    void Subsrcibe(AsyncMessageHandler<T> handler);
    bool Unsubscribe(AsyncMessageHandler<T> handler);

    public bool IsBusy { get; }
}
