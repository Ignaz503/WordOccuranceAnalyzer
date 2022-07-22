using TextFileContentAnalyzer.Testing.Core.Categories;

namespace TextFileContentAnalyzer.Testing.Core.Results;


public interface ITestResult
{
    public ITestCategory Category { get; }

    TestResult Result { get; }

    bool HasPassed => Result == TestResult.Passed;
    bool HasFailed => Result == TestResult.Failed;

}
