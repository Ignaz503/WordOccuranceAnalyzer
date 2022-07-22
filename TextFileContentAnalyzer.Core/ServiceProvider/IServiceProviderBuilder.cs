namespace TextFileContentAnalyzer.Core.ServiceProvider;

/// <summary>
/// Fluent API to build a service provider.
/// </summary>
public interface IServiceProviderBuilder 
{
    /// <summary>
    /// Adds a singleton service based on a given object instance.
    /// </summary>
    /// <typeparam name="T">Type of Service.</typeparam>
    /// <param name="t">Service Instance.</param>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddSingleton<T>(T t) where T : class;

    /// <summary>
    /// Adds a singleton service.
    /// </summary>
    /// <typeparam name="T">Type of Service.</typeparam>
    /// <param name="t">Service Instance.</param>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddSingleton<T>() where T : class;
    /// <summary>
    /// Adds a singleton service based on a factory function.
    /// </summary>
    /// <typeparam name="T">Type of Service.</typeparam>
    /// <param name="factory">Factory for the service.</param>
    /// <param name="dependencies">Dependencies of service as the builder can't figure them out itself.</param>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddSingleton<T>(Func<IServiceProvider, T> factory, params Type[] dependencies) where T : class;

    /// <summary>
    /// Adss a singleton service based on an instance of an object, registered as a different base type.
    /// </summary>
    /// <typeparam name="TAs">The type of the service.</typeparam>
    /// <typeparam name="TInstance">The type of the service instance.</typeparam>
    /// <param name="instance">The service instance.</param>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddSingleton<TAs,TInstance>(TInstance instance)
        where TInstance : class, TAs 
        where TAs : class;
    /// <summary>
    /// Adss a singleton service registered as a different base type.
    /// </summary>
    /// <typeparam name="TAs">The type of the service.</typeparam>
    /// <typeparam name="TInstance">The type of the service instance.</typeparam>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddSingleton<TAs,TInstance>() 
        where TInstance : class, TAs
        where TAs : class;

    /// <summary>
    /// adds a transient service based on a factory function.
    /// </summary>
    /// <typeparam name="T">The service type.</typeparam>
    /// <param name="factory">The service factory.</param>
    /// <param name="dependencies">Dependencies of service as the builder can't figure them out itself.</param>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddTransient<T>(Func<IServiceProvider, T> factory, params Type[] dependencies) where T : class;

    /// <summary>
    /// adds a transient service.
    /// </summary>
    /// <typeparam name="T">Type of the service.</typeparam>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddTransient<T>() where T : class;

    /// <summary>
    /// Adss a transient service registered as a different base type.
    /// </summary>
    /// <typeparam name="TAs">The type of the service.</typeparam>
    /// <typeparam name="TInstance">The type of the service instance.</typeparam>
    /// <returns>The builder to allow for fluent chaining.</returns>
    IServiceProviderBuilder AddTransient<TAs, TInstance>()
        where TInstance : class, TAs
        where TAs : class;

    /// <summary>
    /// Builds the service provider.
    /// </summary>
    /// <returns>A service provider based on the builder.</returns>
    IServiceProvider Build();
}
