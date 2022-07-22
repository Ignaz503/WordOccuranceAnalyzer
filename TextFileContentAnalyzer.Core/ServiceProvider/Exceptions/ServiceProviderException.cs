using System.Runtime.Serialization;

namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public abstract class ServiceProviderException : Exception
{
    protected ServiceProviderException()
    {
    }

    protected ServiceProviderException(string? message) : base(message)
    {
    }

    protected ServiceProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected ServiceProviderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
