namespace TextFileContentAnalyzer.Core.ServiceProvider;


/// <summary>
/// Attribute that indicates which constructor to use when multiple are defined on an object.
/// </summary>
[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
public class ServiceCtorAttribute : Attribute
{ }