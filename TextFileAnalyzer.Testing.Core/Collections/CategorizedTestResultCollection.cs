using System.Collections;
using  TextFileContentAnalyzer.Testing.Core.Categories;
using  TextFileContentAnalyzer.Testing.Core.Results;
namespace TextFileContentAnalyzer.Testing.Core.Collections;

public class CategorizedTestResultCollection : ITestResultCollection
{
    readonly Dictionary<ITestCategory, TestResultCollection> categorizedResults;

    public CategorizedTestResultCollection()
    {
        categorizedResults = new(EqualityComparer<ITestCategory>.Default);
    }

    public int PassedCount
    {
        get
        {
            int c = 0;
            foreach (var category in categorizedResults.Values)
                c += category.PassedCount;
            return c;
        }
    }

    public int FailedCount
    {
        get
        {
            int c = 0;
            foreach (var category in categorizedResults.Values)
                c += category.FailedCount;
            return c;
        }
    }

    public int TotalCount => PassedCount + FailedCount;


    public IEnumerable<KeyValuePair<ITestCategory, TestResultCollection>> Categories => categorizedResults.AsEnumerable();

    public IEnumerable<ITestResult> PassedTests
    {
        get
        {
            foreach (var category in categorizedResults.Values)
                foreach (var passedTest in category.PassedTests)
                    yield return passedTest;
        }
    }

    public IEnumerable<ITestResult> FailedTests
    {
        get
        {
            foreach (var category in categorizedResults.Values)
                foreach (var failedTest in category.FailedTests)
                    yield return failedTest;
        }
    }

    public void Add(ITestResult result)
    {
        if (categorizedResults.ContainsKey(result.Category))
        {
            categorizedResults[result.Category].Add(result);
            return;
        }
        var coll = new TestResultCollection();
        coll.Add(result);
        categorizedResults.Add(result.Category, coll);
    }

    public IEnumerator<ITestResult> GetEnumerator()
    {
        foreach (var category in categorizedResults.Values)
            foreach (var test in category)
                yield return test;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
