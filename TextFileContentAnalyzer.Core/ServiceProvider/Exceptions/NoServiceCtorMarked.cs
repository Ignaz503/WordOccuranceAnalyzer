namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public class NoServiceCtorMarked : ServiceProviderBuilderException
{
    public NoServiceCtorMarked(Type t) : base($"{t.Name} has mutltiple constructors, but no constructor marked as a service constructor. Unable to figure out which to use for instantiation")
    { }
}