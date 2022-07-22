namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

/// <summary>
/// Instantiates a singleton service.
/// </summary>
internal class SingletonCtorServiceInstantiaor : CtorServiceInstantiator
{
    private Lazy<object> _instance;


    public SingletonCtorServiceInstantiaor(Type serviceTypeActual, Type serviceTypeRegsitered, IReadOnlyCollection<Type> dependencies, IServiceProvider provider) : base(serviceTypeActual, serviceTypeRegsitered, dependencies, provider)
    {
        _instance = new(CreateInstance);
    }

    public override T Get<T>()
        => (T)Get(typeof(T));

    public override object Get(Type t)
        => _instance.Value;
}
