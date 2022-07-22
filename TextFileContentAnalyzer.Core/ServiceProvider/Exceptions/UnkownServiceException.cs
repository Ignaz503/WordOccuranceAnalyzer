namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public class UnkownServiceException : ServiceProviderException
{
    public UnkownServiceException(Type t):base($"{t} is an unkown service.")
    {

    }
}