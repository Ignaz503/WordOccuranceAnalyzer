using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;

/// <summary>
/// Base execution context for a word occurrence counter.
/// </summary>
public class WordOccurrenceCounterExecutionContextBase
{
    public CancellationToken CancellationToken { get; private set; }
    public IWordOccurrenceCounter OccuranceCounter { get; private set; }
    public Stream Stream { get; private set; }

    public WordOccurrenceCounterExecutionContextBase(IWordOccurrenceCounter occurrenceCounter, Stream stream, CancellationToken cancellationToken)
    {
        CancellationToken = cancellationToken;
        OccuranceCounter = occurrenceCounter;
        Stream = stream;
    }
}