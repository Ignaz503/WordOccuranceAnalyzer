using TextFileContentAnalyzer.Core.Optional;

namespace TextFileContentAnalyzer.Core.DataAnalyzer;
/// <summary>
/// interface for a generic data analyzer
/// </summary>
/// <typeparam name="TExecutionContext">the execution context of the data analyzer</typeparam>
public interface IDataAnalyzer<TExecutionContext>
{
    Result<Okay,Exception> Analyze(TExecutionContext ctx);
}
