using System.Reflection;
using TextFileContentAnalyzer.Testing.Core.Categories;
using TextFileContentAnalyzer.Testing.Core.Results;

namespace TextFileContentAnalyzer.Testing.Core;

/// <summary>
/// Runs a test from a certain test class
/// </summary>
public class TestRunner : ITestRunner
{
    public Type TestClass { get; private set; }
    public MethodInfo TestMethod { get; private set; }

    public bool IsAsync { get; private set; }

    public TestRunner(Type testClass, MethodInfo testMethod, bool isAsync)
    {
        this.TestClass = testClass;
        this.TestMethod = testMethod;
        this.IsAsync = isAsync;
    }

    /// <summary>
    /// Executes the test for this runner.
    /// </summary>
    /// <param name="ctx">The execution context.</param>
    /// <returns>An awaitable task.</returns>
    public async Task Run(TestContext ctx) 
    {
        var obj = Activator.CreateInstance(TestClass);
        try
        {
            if (!IsAsync)
            {
                
                TestMethod.Invoke(obj, null);
                ctx.Reporter.Add(new Passed(this, GetCategory(ctx.CategoryFactory)));
            }
            else 
            {
                var task = (Task)TestMethod.Invoke(obj, null)!;
                await task;
                ctx.Reporter.Add(new Passed(this, GetCategory(ctx.CategoryFactory)));
            }
        }
        catch(Exception ex)
        {
            ctx.Reporter.Add(new Failed<Exception>(this, ex, GetCategory(ctx.CategoryFactory)));
        }finally
        {
            if (obj is IDisposable disposableTest)
                disposableTest.Dispose();
            else if (obj is IAsyncDisposable asyncDispose)
                await asyncDispose.DisposeAsync();
        }
    }

    ITestCategory GetCategory(ITestCategoryFactory factory) 
        => factory.CreateCategory(this);

}
