using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;
using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

/// <summary>
/// Implementation for service provider.
/// </summary>
public class ServiceProvider : IServiceProvider
{
    readonly Dictionary<Type, IServiceInstantiator> services;


    public ServiceProvider()
    {
        services = new();
    }

    public T GetService<T>()
        where T : class => (GetService(typeof(T)) as T)!;

    public object GetService(Type serviceType)
    {
        if (!services.TryGetValue(serviceType, out var instantiator))
            throw new UnkownServiceException(serviceType);
        return instantiator.Get(serviceType);
    }

    /// <summary>
    /// adds a service to the service provider.
    /// Used by service provider builder.
    /// </summary>
    /// <param name="t">Type of service.</param>
    /// <param name="instantiator">The service instantiator.</param>
    internal void AddService(Type t, IServiceInstantiator instantiator) => services.Add(t, instantiator);

}
