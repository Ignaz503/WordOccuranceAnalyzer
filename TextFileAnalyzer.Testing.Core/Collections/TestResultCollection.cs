using System.Collections;
using TextFileContentAnalyzer.Testing.Core.Results;

namespace TextFileContentAnalyzer.Testing.Core.Collections;


public class TestResultCollection : ITestResultCollection
{
    readonly List<ITestResult> passedTests;
    readonly List<ITestResult> failedTests;

    public IEnumerable<ITestResult> PassedTests => passedTests;
    public IEnumerable<ITestResult> FailedTests => failedTests;


    public int PassedCount => passedTests.Count;
    public int FailedCount => failedTests.Count;
    public int TotalCount => PassedCount + FailedCount;


    public TestResultCollection()
    {
        passedTests = new();
        failedTests = new();
    }

    public void Add(ITestResult result)
    {
        if (result.HasPassed)
            passedTests.Add(result);
        else
            failedTests.Add(result);
    }

    public IEnumerator<ITestResult> GetEnumerator()
    {
        foreach (var test in passedTests)
            yield return test;
        foreach (var test in failedTests)
            yield return test;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
