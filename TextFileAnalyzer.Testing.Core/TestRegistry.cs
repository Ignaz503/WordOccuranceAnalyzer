using System.Reflection;

namespace TextFileContentAnalyzer.Testing.Core;

/// <summary>
/// Registry for all tests found.
/// </summary>
public static class TestRegistry 
{
    static IEnumerable<ITestRunner>? allTests;


    /// <summary>
    /// Builds the registry from all tests found in the provided assembly.
    /// </summary>
    /// <param name="assemblyContainingTests">The assembly test should be located in.</param>
    public static void BuildRegistry(Assembly assemblyContainingTests) 
    { 
        var taskType = typeof(Task);

        var types = assemblyContainingTests.GetTypes()
            .Where(t => t.IsDefined(typeof(TestAttribute)));

        var tests = new List<TestRunner>();

        foreach (var type in types) 
        {
            var testMethods = type.GetMethods().Where(m => m.GetCustomAttribute<FactAttribute>() is not null && m.IsPublic && !m.IsStatic && !m.IsGenericMethod && !m.IsConstructor);

            foreach (var method in testMethods) 
            {
                bool isAsync = method.ReturnType.Equals(taskType);
                tests.Add(new TestRunner(type,method,isAsync));
            }
        }

        allTests = tests;
    }

    /// <summary>
    /// Exectues all tests.
    /// </summary>
    /// <param name="ctx">The context of the test execution.</param>
    /// <returns>An awaitable task.</returns>
    public static async Task Execute(TestContext ctx) 
    {
        if (allTests is null)
            return;
        foreach (var test in allTests) 
        {
            await test.Run(ctx);
        }
    }
}