namespace TextFileContentAnalyzer.Testing.Core;

/// <summary>
/// Attribute given to class that contains test.
/// Needed to locate test classes.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TestAttribute : Attribute
{
}
