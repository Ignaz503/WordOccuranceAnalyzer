using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;

namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;

/// <summary>
/// Base execution context for a word occurance counter.
/// </summary>
public class WordOccuranceCounterExecutionContextBase
{
    public CancellationToken CancellationToken { get; private set; }
    public IWordOccuranceCounter OccuranceCounter { get; private set; }
    public Stream Stream { get; private set; }

    public WordOccuranceCounterExecutionContextBase(IWordOccuranceCounter occuranceCounter, Stream stream, CancellationToken cancellationToken)
    {
        CancellationToken = cancellationToken;
        OccuranceCounter = occuranceCounter;
        Stream = stream;
    }
}