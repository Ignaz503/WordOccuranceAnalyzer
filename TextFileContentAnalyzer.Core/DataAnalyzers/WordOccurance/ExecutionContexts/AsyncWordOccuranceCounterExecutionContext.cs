using TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.Collections;
using TextFileContentAnalyzer.Core.Util;
namespace TextFileContentAnalyzer.Core.DataAnalyzers.WordOccurance.ExecutionContexts;

/// <summary>
/// Execution context for a WordOccuranceAnalyzer.
/// Defines the progress report handler, as well as
/// a CancellationToken, an OccuranceCounter and the Stream the data is from.
/// </summary>
public class AsyncWordOccuranceCounterExecutionContext : WordOccuranceCounterExecutionContextBase
{

    public IAsyncProgressReport<long> Progress { get; private set; }

    public AsyncWordOccuranceCounterExecutionContext(IAsyncProgressReport<long> progress, IWordOccuranceCounter occuranceCounter, Stream stream, CancellationToken cancellationToken)
        : base(occuranceCounter, stream, cancellationToken)
    {
        Progress = progress ?? throw new ArgumentNullException(nameof(progress));
    }
}
