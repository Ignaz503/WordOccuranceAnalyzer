namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public class InvalidNodeRegisteredExteption : ServiceProviderBuilderException
{
    public InvalidNodeRegisteredExteption(Type serviceType)
        : base($"The service {serviceType} has an invalid registration. Check for circular dependencies or missing dependencies")
    {

    }
}
