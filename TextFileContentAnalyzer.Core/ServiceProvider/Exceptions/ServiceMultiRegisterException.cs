namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public class ServiceMultiRegisterException : ServiceProviderBuilderException 
{
    public ServiceMultiRegisterException(Type s) :base($"{s} is already registered as a service.")
    {

    }
}