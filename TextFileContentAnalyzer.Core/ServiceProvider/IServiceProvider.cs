namespace TextFileContentAnalyzer.Core.ServiceProvider;

/// <summary>
/// A service proivder.
/// </summary>
public interface IServiceProvider : System.IServiceProvider
{
    /// <summary>
    /// Gets a service from the provider.
    /// </summary>
    /// <typeparam name="T">Type of the service.</typeparam>
    /// <returns>An instance of the service.</returns>
    T GetService<T>()
         where T : class;
}
