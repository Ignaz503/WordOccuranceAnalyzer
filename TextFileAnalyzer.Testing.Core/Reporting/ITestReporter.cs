using TextFileContentAnalyzer.Testing.Core.Results;

namespace TextFileContentAnalyzer.Testing.Core.Reporting;

/// <summary>
/// Interface to report test results.
/// </summary>
public interface ITestReporter
{
    int PassedCount { get; }
    int FailedCount { get; }

    void Add(ITestResult result);

    public void DisplayResults();
}
