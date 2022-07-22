namespace TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

internal class CtorServiceInstantiator : IServiceInstantiator
{
    private readonly IServiceProvider _provider;
    private readonly Type serviceTypeActual;
    private readonly Type serviceTypeRegsitered;
    private readonly IReadOnlyCollection<Type> dependencies;

    public CtorServiceInstantiator(Type serviceTypeActual, Type serviceTypeRegsitered, IReadOnlyCollection<Type> dependencies, IServiceProvider provider)
    {
        this.serviceTypeActual = serviceTypeActual;
        this.serviceTypeRegsitered = serviceTypeRegsitered;
        this.dependencies = dependencies;
        _provider = provider;
    }

    public virtual T Get<T>()
        where T : class
        => (T)Get(typeof(T));


    public virtual object Get(Type t)
    {
        ServiceInstantiatorValidator.Validate(t,serviceTypeRegsitered);
        return CreateInstance();
    }

    protected object CreateInstance()
        => Activator.CreateInstance(serviceTypeActual, GetDependencies())!;


    protected object[] GetDependencies()
    {
        var services = new object[dependencies.Count];
        var i = 0;
        foreach (var dependency in dependencies)
        {
            services[i] = _provider.GetService(dependency)!;
            i++;
        }
        return services;
    }
}
