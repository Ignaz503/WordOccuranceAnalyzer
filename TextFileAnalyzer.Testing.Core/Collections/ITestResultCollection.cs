using TextFileContentAnalyzer.Testing.Core.Results;

namespace TextFileContentAnalyzer.Testing.Core.Collections;

public interface ITestResultCollection : IEnumerable<ITestResult>
{
    int PassedCount { get; }
    int FailedCount { get; }

    int TotalCount { get; }

    public IEnumerable<ITestResult> PassedTests { get; }
    public IEnumerable<ITestResult> FailedTests { get; }

    public void Add(ITestResult result);

}
