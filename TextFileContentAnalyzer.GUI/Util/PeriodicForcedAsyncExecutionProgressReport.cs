using System;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.Util;

namespace TextFileContentAnalyzer.GUI.Util;

/// <summary>
/// Forces context switch away from synchronization context
/// potentially captured by IProgress<<typeparamref name="T"/>>
/// but only periodically to avoid context switching every time
/// </summary>
/// <typeparam name="T">Type of progress to report</typeparam>
public class PeriodicForcedAsyncExecutionProgressReport<T> : IAsyncProgressReport<T>
{
    TickBasedTimer timer;
    IProgress<T> syncedProgressReporter;


    public PeriodicForcedAsyncExecutionProgressReport(TimeSpan period, Action<T> handler)
    {
        timer = new() { MillisecondsToFire = period.Milliseconds };
        syncedProgressReporter = new Progress<T>(handler);

    }

    public async Task Report(T value)
    {
        if (timer.Meassure())
            syncedProgressReporter.Report(value);

        await Task.Yield();
    }
}

