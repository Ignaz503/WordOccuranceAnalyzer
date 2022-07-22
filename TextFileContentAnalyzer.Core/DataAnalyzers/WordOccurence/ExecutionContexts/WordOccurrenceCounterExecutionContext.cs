using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.Collections;
using TextFileContentAnalyzer.Core.Mediator;
namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurrence.ExecutionContexts;

/// <summary>
/// Execution context for a non async word occurrence counter.
/// </summary>
public class WordOccurrenceCounterExecutionContext : WordOccurrenceCounterExecutionContextBase
{
    public IPublisher<long> ProgressPublisher { get; init; }
    public WordOccurrenceCounterExecutionContext(IPublisher<long> progressPublisher, IWordOccurrenceCounter occurrenceCounter, Stream stream, CancellationToken cancellationToken) : base(occurrenceCounter, stream, cancellationToken)
    {
        ProgressPublisher = progressPublisher;
    }

}
