using TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

/// <summary>
/// Instantiates a singleton service based on a given object instance.
/// </summary>
/// <typeparam name="K">The type of the service.</typeparam>
internal class SingletonObjectServiceInstantiator<K> : IServiceInstantiator
    where K : class
{
    static readonly Type objectType = typeof(K);
    readonly K _instance;

    public SingletonObjectServiceInstantiator(K obj)
    {
        _instance = obj;
    }

    public T Get<T>()
        where T: class
    {
        ServiceInstantiatorValidator.Validate(typeof(T), objectType);
        return (_instance as T)!;
    }

    public object Get(Type t)
    {
        ServiceInstantiatorValidator.Validate(t, objectType);
        return _instance!;
    }
}
