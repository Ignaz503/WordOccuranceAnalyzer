using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileContentAnalyzer.Core.Mediator;

/// <summary>
/// Synchrounous mediator to allow for object communication without tight coupling
/// </summary>
/// <typeparam name="T">Type of messages being sent</typeparam>
public interface IMediator<T>  : IPublisher<T>
{
    void Subsrcibe(MessageHandler<T> handler);
    bool Unsubscribe(MessageHandler<T> handler);

    bool IsBusy { get; }

}


