using TextFileContentAnalyzer.Testing.Core.Collections;
using TextFileContentAnalyzer.Testing.Core.Results;

namespace TextFileContentAnalyzer.Testing.Core.Reporting;

/// <summary>
/// Reports tests to the console as output, all tests are categorized
/// </summary>
public class CategorizedConsoleTestReporter : ITestReporter
{

    CategorizedTestResultCollection testResults;

    public int PassedCount => testResults.PassedCount;

    public int FailedCount => testResults.FailedCount;

    public CategorizedConsoleTestReporter()
    {
        testResults = new();
    }

    public void Add(ITestResult result)
    {
        testResults.Add(result);
    }

    public void DisplayResults()
    {
        foreach (var category in testResults.Categories)
        {
            Console.WriteLine($"---------{category.Key}------------");
            PrintTestResultCollection(category.Value);
            Console.WriteLine();
        }

        Console.WriteLine($"---------Summary------------");
        Console.WriteLine($"Total Tests: {testResults.TotalCount} Passed: {PassedCount}  Failed: {FailedCount}");

    }

    private void PrintTestResultCollection(TestResultCollection collection)
    {
        foreach (var test in collection.PassedTests)
            PrintPassedTestResult(test);
        foreach (var test in collection.FailedTests)
            PrintFailedTestResult(test);
        Console.WriteLine($"Tests in category: {collection.TotalCount} Passed: {collection.PassedCount}  Failed: {collection.FailedCount}");
    }

    private void PrintFailedTestResult(ITestResult testResult)
        => PrintTestResultWithHeader(ConsoleColor.Red, "[X]", testResult);

    private void PrintPassedTestResult(ITestResult testResult)
        => PrintTestResultWithHeader(ConsoleColor.Green, "[J]", testResult);

    void PrintHeader(ConsoleColor headerColor, string headerContent)
    {
        var col = Console.ForegroundColor;
        Console.ForegroundColor = headerColor;
        Console.Write(headerContent);
        if (!headerContent.EndsWith(' '))
            Console.Write(' ');
        Console.ForegroundColor = col;
    }

    void PrintTestResultWithHeader(ConsoleColor headerColor, string headerContent, ITestResult testResult)
    {
        PrintHeader(headerColor, headerContent);
        Console.WriteLine(testResult.ToString());
    }

}
