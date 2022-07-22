using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.Util;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;

namespace TextFileContentAnalyzer.GUI.Services.WordOccuranceAnalyzerRunners;

public class AsyncWordOccuranceAnalyzerRunner : IWordOccuranceAnalyzationRunner
{
    readonly IAsyncDataAnalyzer<AsyncWordOccuranceCounterExecutionContext> _occuranceAnalzyer;

    public AsyncWordOccuranceAnalyzerRunner(IAsyncDataAnalyzer<AsyncWordOccuranceCounterExecutionContext> occuranceAnalzyer)
    {
        _occuranceAnalzyer = occuranceAnalzyer;
    }

    public async Task Run(Stream stream, IWordOccuranceCounter occuranceCounter, IAsyncProgressReport<long> onProgess, IProgress<Result<Okay, Exception>> onFinished, CancellationToken ct)
    {
        var res = await _occuranceAnalzyer.Analyze(new(onProgess, occuranceCounter, stream, ct));
        onFinished.Report(res);
    }
}
