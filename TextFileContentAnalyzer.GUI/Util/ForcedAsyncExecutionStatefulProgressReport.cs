using System;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.Util;

namespace TextFileContentAnalyzer.GUI.Util;

/// <summary>
/// Forces context switch away from synchronization context
/// potentially captured by IProgress<<typeparamref name="T"/>>
/// </summary>
/// <typeparam name="T">Type of progress to report</typeparam>
public class ForcedAsyncExecutionStatefulProgressReport<T> : IAsyncProgressReport<T>
{
    IProgress<T> syncedProgressReporter;

    public ForcedAsyncExecutionStatefulProgressReport(Action<T> handler)
    {
        syncedProgressReporter = new Progress<T>(handler);
    }

    public async Task Report(T value)
    {
        syncedProgressReporter.Report(value);
        await Task.Yield();
    }
}

