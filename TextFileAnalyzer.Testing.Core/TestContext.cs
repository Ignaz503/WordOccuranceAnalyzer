using  TextFileContentAnalyzer.Testing.Core.Categories;
using  TextFileContentAnalyzer.Testing.Core.Reporting;

namespace TextFileContentAnalyzer.Testing.Core;

/// <summary>
/// Defines the context of test execution.
/// </summary>
public struct TestContext
{
    public ITestCategoryFactory CategoryFactory { get; init; }
    public ITestReporter Reporter { get; init; }
}
