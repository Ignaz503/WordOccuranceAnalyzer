namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

/// <summary>
/// Instantiates a service.
/// </summary>
internal interface IServiceInstantiator
{
    /// <summary>
    /// Get the service of the type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of service to instantiate.</typeparam>
    /// <returns>An instance of the service.</returns>
    T Get<T>()
        where T : class;

    /// <summary>
    /// Get the service of the type <paramref name="t"/>.
    /// </summary>
    /// <param name="t">Type of service to instantiate.</param>
    /// <returns>An instance of the service.</returns>
    public object Get(Type t);

}
