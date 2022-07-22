namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

/// <summary>
/// Instantiates a singleton service based on a factory function.
/// </summary>
/// <typeparam name="K">The type of the service.</typeparam>
internal class SingletonFactoryServiceInstantiator<K> : FactoryServiceInstantiator<K>
    where K : class
{
    Lazy<K> _instance;

    public SingletonFactoryServiceInstantiator(IServiceProvider provider, Func<IServiceProvider, K> factory) :base(provider,factory)
    {
        _instance = new(CreateInstance);

    }

    public override T Get<T>() where T : class
        => (Get(typeof(T)) as T)!;

    public override object Get(Type t)
    {
        ServiceInstantiatorValidator.Validate(t, serviceType);
        return _instance.Value;
    }

}