using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.Util;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;

namespace TextFileContentAnalyzer.GUI.Services.WordOccurrenceAnalyzerRunners;

public class AsyncWordOccurrenceAnalyzerRunner : IWordOccurrenceAnalyzationRunner
{
    readonly IAsyncDataAnalyzer<AsyncWordOccurrenceCounterExecutionContext> _occurrenceAnalzyer;

    public AsyncWordOccurrenceAnalyzerRunner(IAsyncDataAnalyzer<AsyncWordOccurrenceCounterExecutionContext> occurrenceAnalzyer)
    {
        _occurrenceAnalzyer = occurrenceAnalzyer;
    }

    public async Task Run(Stream stream, IWordOccurrenceCounter occurrenceCounter, IAsyncProgressReport<long> onProgess, IProgress<Result<Okay, Exception>> onFinished, CancellationToken ct)
    {
        var res = await _occurrenceAnalzyer.Analyze(new(onProgess, occurrenceCounter, stream, ct));
        onFinished.Report(res);
    }
}
