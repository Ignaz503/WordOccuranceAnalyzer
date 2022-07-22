using System.Runtime.Serialization;

namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public abstract class ServiceProviderInstantiatorException : ServiceProviderException
{
    protected ServiceProviderInstantiatorException()
    {
    }

    protected ServiceProviderInstantiatorException(string? message) : base(message)
    {
    }

    protected ServiceProviderInstantiatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected ServiceProviderInstantiatorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
