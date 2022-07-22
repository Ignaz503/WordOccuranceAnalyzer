using TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

/// <summary>
/// helper function to validate if instantiation request is valid.
/// </summary>
public static class ServiceInstantiatorValidator 
{
    /// <summary>
    /// Validate if requested type is equal to service type.
    /// </summary>
    /// <param name="desired">Type to be instantiated.</param>
    /// <param name="registerd">The type registered.</param>
    /// <exception cref="InvalidGetRequestException">Thrown if <paramref name="desired"/> not equal <paramref name="registerd"/>.</exception>
    public static void Validate(Type desired, Type registerd) 
    {
        if (!desired.Equals(registerd))
            throw new InvalidGetRequestException(desired, registerd);
    }
}
