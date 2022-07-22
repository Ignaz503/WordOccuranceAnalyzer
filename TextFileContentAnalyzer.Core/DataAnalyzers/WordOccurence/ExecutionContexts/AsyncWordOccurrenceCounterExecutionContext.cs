using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
using TextFileContentAnalyzer.Core.Util;
namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;

/// <summary>
/// Execution context for a WordOccurrenceAnalyzer.
/// Defines the progress report handler, as well as
/// a CancellationToken, an OccuranceCounter and the Stream the data is from.
/// </summary>
public class AsyncWordOccurrenceCounterExecutionContext : WordOccurrenceCounterExecutionContextBase
{

    public IAsyncProgressReport<long> Progress { get; private set; }

    public AsyncWordOccurrenceCounterExecutionContext(IAsyncProgressReport<long> progress, IWordOccurrenceCounter occurrenceCounter, Stream stream, CancellationToken cancellationToken)
        : base(occurrenceCounter, stream, cancellationToken)
    {
        Progress = progress ?? throw new ArgumentNullException(nameof(progress));
    }
}
