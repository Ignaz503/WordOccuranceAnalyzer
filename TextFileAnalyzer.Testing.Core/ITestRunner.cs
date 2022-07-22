namespace TextFileContentAnalyzer.Testing.Core;

/// <summary>
/// Interface to run a test with a certain context.
/// </summary>
public interface ITestRunner 
{
    Task Run(TestContext ctx);
}
