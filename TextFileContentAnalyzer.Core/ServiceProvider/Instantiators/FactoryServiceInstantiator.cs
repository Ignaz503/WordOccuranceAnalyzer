namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

internal class FactoryServiceInstantiator<K> : IServiceInstantiator 
    where K : class
{
    protected static readonly Type serviceType = typeof(K);
    protected readonly Func<IServiceProvider, K> _factory;
    readonly IServiceProvider _provider;
    public FactoryServiceInstantiator(IServiceProvider provider, Func<IServiceProvider,K> factory)
    {
        this._provider = provider;
        this._factory = factory;
    }

    public virtual T Get<T>() where T : class
        => (Get(typeof(T)) as T)!;

    public virtual object Get(Type t)
    {
        ServiceInstantiatorValidator.Validate(t, serviceType);
        return CreateInstance();
    }

    protected K CreateInstance() 
    {
        return _factory(_provider);
    }

}
