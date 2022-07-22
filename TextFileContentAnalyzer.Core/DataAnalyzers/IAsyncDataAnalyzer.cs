using TextFileContentAnalyzer.Core.Optional;

namespace TextFileContentAnalyzer.Core.DataAnalyzer;

/// <summary>
/// interface for a generic data analyzer that can run asyncronously
/// </summary>
/// <typeparam name="TExecutionContext">the execution context of the data analyzer</typeparam>
public interface IAsyncDataAnalyzer<TExecutionContext>
{
    Task<Result<Okay,Exception>> Analyze(TExecutionContext ctx);
}
