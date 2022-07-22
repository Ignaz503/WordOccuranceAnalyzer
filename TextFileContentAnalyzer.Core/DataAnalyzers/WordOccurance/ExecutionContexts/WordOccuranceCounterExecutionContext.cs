using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;
using TextFileContentAnalyzer.Core.Mediator;
namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;

/// <summary>
/// Execution context for a non async word occurance counter.
/// </summary>
public class WordOccuranceCounterExecutionContext : WordOccuranceCounterExecutionContextBase
{
    public IPublisher<long> ProgressPublisher { get; init; }
    public WordOccuranceCounterExecutionContext(IPublisher<long> progressPublisher, IWordOccuranceCounter occuranceCounter, Stream stream, CancellationToken cancellationToken) : base(occuranceCounter, stream, cancellationToken)
    {
        ProgressPublisher = progressPublisher;
    }

}
