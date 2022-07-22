using System;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.Mediator;

namespace TextFileContentAnalyzer.GUI.Util;

/// <summary>
/// published progress periodicly instead of immediate
/// avoids synchronization context switching
/// </summary>
/// <typeparam name="T">Type of content to publish</typeparam>
public class PeriodicProgressPublisher<T> : IPublisher<T>
{
    readonly LazyMediatorOneMessage<T> progressMediator;
    readonly PeriodicTimer timer;

    public PeriodicProgressPublisher(TimeSpan period, params AsyncMessageHandler<T>[] messageHandlers)
    {
        progressMediator = new();
        foreach (var messageHandler in messageHandlers)
            progressMediator.Subsrcibe(messageHandler);
        timer = new(period);

    }

    public async Task Run(CancellationToken ct)
    {

        while (await timer.WaitForNextTickAsync(ct))
        {
            if (ct.IsCancellationRequested)
                break;

            await progressMediator.ProccessMessages();
        }
        Stop();
    }

    public void Stop()
    {
        timer.Dispose();
    }


    public void Publish(T message)
    {
        progressMediator.Publish(message);
    }
}
