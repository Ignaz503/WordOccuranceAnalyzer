using TextFileContentAnalyzer.Testing.Core.Categories;

namespace TextFileContentAnalyzer.Testing.Core.Results;

/// <summary>
/// Describes a test that has passed
/// </summary>
public class Passed : ITestResult
{
    public TestRunner Test { private get; init; }

    public TestResult Result => TestResult.Passed;

    public ITestCategory Category { get; private set; }

    public Passed(TestRunner suit, ITestCategory category)
    {
        Test = suit;
        Category = category;
    }

    public override string ToString()
        => $"{Test.TestMethod.Name} passed";

}
