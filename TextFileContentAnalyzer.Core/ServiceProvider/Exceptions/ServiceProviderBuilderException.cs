using System.Runtime.Serialization;

namespace TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

public class ServiceProviderBuilderException : ServiceProviderException
{
    public ServiceProviderBuilderException()
    {
    }

    public ServiceProviderBuilderException(string? message) : base(message)
    {
    }

    public ServiceProviderBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ServiceProviderBuilderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
