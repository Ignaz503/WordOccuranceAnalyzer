using System.Text;
using TextFileContentAnalyzer.Testing.Core.Categories;

namespace TextFileContentAnalyzer.Testing.Core.Results;

/// <summary>
/// Describes a test that has failed and why.
/// </summary>
/// <typeparam name="T">Type of failure.</typeparam>
public class Failed<T> : ITestResult
{
    public TestRunner Test { private get; init; }
    public T Exception { private get; init; }
    public ITestCategory Category { get; private set; }

    public Failed(TestRunner test, T exception, ITestCategory category)
    {
        Test = test;
        Exception = exception;
        Category = category;
    }

    public TestResult Result => TestResult.Failed;

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder
            .Append(Test.TestMethod.Name)
            .AppendLine(" failed");

        if (Exception is null)
            return builder.ToString();
        builder.AppendLine("Reason:")
             .Append(Exception.ToString());
        return builder.ToString();
    }
}
