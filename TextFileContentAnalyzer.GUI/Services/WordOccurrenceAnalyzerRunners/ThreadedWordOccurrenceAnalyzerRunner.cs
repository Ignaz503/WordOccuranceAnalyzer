using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.DataAnalyzer;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
using TextFileContentAnalyzer.Core.Optional;
using TextFileContentAnalyzer.Core.Util;
using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;
using TextFileContentAnalyzer.GUI.Util;

namespace TextFileContentAnalyzer.GUI.Services.WordOccurrenceAnalyzerRunners;

public class ThreadedWordOccurrenceAnalyzerRunner : IWordOccurrenceAnalyzationRunner
{
    readonly IDataAnalyzer<WordOccurrenceCounterExecutionContext> _occurrenceAnalyzer;
    readonly IProgressFrequencyProvider updateFrequencyProvider;

    public ThreadedWordOccurrenceAnalyzerRunner(IDataAnalyzer<WordOccurrenceCounterExecutionContext> occurrenceAnalyzer, IProgressFrequencyProvider updateFrequencyProvider)
    {
        _occurrenceAnalyzer = occurrenceAnalyzer;
        this.updateFrequencyProvider = updateFrequencyProvider;
    }

    public async Task Run(Stream stream, IWordOccurrenceCounter counter, IAsyncProgressReport<long> onProgess, IProgress<Result<Okay, Exception>> onFinished, CancellationToken ct)
    {
        try
        {
            var activeFrequency = updateFrequencyProvider.Frequency;
            var periodicProgress = new PeriodicProgressPublisher<long>(activeFrequency, onProgess.Report);
            var ctx = new WordOccurrenceCounterExecutionContext(periodicProgress, counter, stream, ct);

#pragma warning disable CS4014
            Task.Run(() => periodicProgress.Run(ct), ct);
#pragma warning restore
            var signal = new Signal_()
            {
                IsDone = false,
                Result = default,
            };

            var thread = new Thread(() =>
            {
                try
                {
                    signal.Result = _occurrenceAnalyzer.Analyze(ctx);
                }
                catch (Exception ex)
                {
                    signal.Result = ex.From<Okay, Exception>();
                }
                finally
                {
                    signal.IsDone = true;
                }

            })
            {
                IsBackground = true
            };
            thread.Start();

            while (!signal.IsDone)
                await Task.Delay(activeFrequency, ct);
            periodicProgress.Stop();
            onFinished.Report(signal.Result);
        }
        catch(Exception ex) 
        {
            onFinished.Report(ex.From<Okay, Exception>());
        }
    }


    class Signal_
    {
        public bool IsDone;
        public Result<Okay, Exception> Result;
    }

}
