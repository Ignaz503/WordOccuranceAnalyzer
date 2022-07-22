namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public class InvalidGetRequestException : ServiceProviderInstantiatorException
{
    public InvalidGetRequestException(Type t, Type regsiterdService) : base($"{t} must be equal to the registered type {regsiterdService}")
    {

    }
}
