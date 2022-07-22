using System.Reflection;
using TextFileContentAnalyzer.Core.ServiceProvider.Exceptions;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

/// <summary>
/// Implementation of IServiceProviderBuilder.
/// </summary>
public partial class ServiceProviderBuilder : IServiceProviderBuilder
{
    Dictionary<Type, Node> nodes;

    Node? currentNode;

    /// <summary>
    /// The Current Node being added to the builder.
    /// </summary>
    Node? CurrentNode
    {
        get => currentNode;
        set
        {
            if (currentNode != null)
            {
                nodes.Add(currentNode.ServiceType, currentNode);
            }
            currentNode = value;
        }
    }

    public ServiceProviderBuilder()
    {
        nodes = new();
    }

    public IServiceProviderBuilder AddSingleton<T>(T t) 
        where T : class
    {
        EnsureNoMultiRegister(typeof(T));
        CurrentNode = new SingletonObjectNode<T>(t);
        return this;
    }

    public IServiceProviderBuilder AddSingleton<T>() 
        where T :class
    {
        var type = typeof(T);
        EnsureNoMultiRegister(type);
        CurrentNode = new SingletonCtorNode(type, type);
        FigureOutDependenciesFromCtor();
        return this;
    }

    public IServiceProviderBuilder AddSingleton<T>(Func<IServiceProvider, T> factory, params Type[] dependencies)
        where T : class
    {
        var type = typeof(T);
        EnsureNoMultiRegister(type);
        CurrentNode = new SingletonFactoryNode<T>(factory, type, type);

        foreach (var dependentType in dependencies)
            CurrentNode.AddDependency(type);

        return this;
    }

    public IServiceProviderBuilder AddSingleton<TAs, TInstance>(TInstance instance)
        where TInstance : class, TAs
        where TAs : class
    {
        var type = typeof(TAs);
        EnsureNoMultiRegister(type);
        CurrentNode = new SingletonObjectNode<TAs>(instance, type);
        return this;
    }

    public IServiceProviderBuilder AddSingleton<TAs, TInstance>() 
        where TInstance :class, TAs
        where TAs : class
    {
        var type = typeof(TAs);
        EnsureNoMultiRegister(type);
        CurrentNode = new SingletonCtorNode(typeof(TInstance),type);
        FigureOutDependenciesFromCtor();
        return this;
    }


    public IServiceProvider Build()
    {
        //add alst node to nodes
        if (CurrentNode is not null)
            nodes.Add(CurrentNode.ServiceType,CurrentNode);

        Validate();

        var provider = new ServiceProvider();
        
        foreach (var serviceNode in nodes) 
        {
            var type = serviceNode.Key;
            var service = serviceNode.Value;
            provider.AddService(type,service.GetInstantiator(provider));
        }
        return provider;
    }

    /// <summary>
    /// validates all nodes in builder, e.g no circular dependencies.
    /// </summary>
    /// <exception cref="InvalidNodeRegisteredExteption">Throw if invalid node was detected.</exception>
    protected void Validate() 
    {
        foreach (var node in nodes.Values)
        {
            var valid = node.Validate(this);

            if (!valid) 
            {
                throw new InvalidNodeRegisteredExteption(node.ServiceType);
            }

        }
    }

    /// <summary>
    /// Uses reflection to figure out dependencies for a service based on the constructor.
    /// </summary>
    /// <exception cref="NoServiceCtorMarked">Thrown if service type has multiple constructors defined and non is marked for use by the service provider.</exception>
    private void FigureOutDependenciesFromCtor()
    {
        if (CurrentNode is null)
            return;

        var ctors = CurrentNode.ImplementorType.GetConstructors();

        ConstructorInfo? serviceConstructor = null;

        if (ctors.Length > 1)
        {
            foreach (var ctor in ctors)
            {
                if (ctor.GetCustomAttribute<ServiceCtorAttribute>() != null)
                {
                    serviceConstructor = ctor;
                    break;
                }
            }
            if (serviceConstructor == null)
                throw new NoServiceCtorMarked(CurrentNode.ImplementorType);
        }
        else
        {
            serviceConstructor = ctors[0];
        }

        foreach (var param in serviceConstructor.GetParameters())
        {
            CurrentNode.AddDependency(param.ParameterType);
        }

    }

    /// <summary>
    /// ensures a service type hasn't been registerd before.
    /// </summary>
    /// <param name="t">type of service</param>
    /// <exception cref="ServiceMultiRegisterException">Thrown if service already registered.</exception>
    private void EnsureNoMultiRegister(Type t) 
    {
        if (nodes.ContainsKey(t) || CurrentNode?.ServiceType == t)
            throw new ServiceMultiRegisterException(t);
    }

    /// <summary>
    /// returns a Node for a given type.
    /// </summary>
    /// <param name="dependencyType">The type of the service.</param>
    /// <returns>The node if found, null otherwise</returns>
    private Node? GetNode(Type dependencyType)
    {
        if (nodes.TryGetValue(dependencyType, out var value))
            return value;
        return null;
    }

    public IServiceProviderBuilder AddTransient<T>(Func<IServiceProvider, T> factory, params Type[] dependencies) where T : class
    {
        var type = typeof(T);
        EnsureNoMultiRegister(type);
        CurrentNode = new TransientFactoryNode<T>(factory, type, type);
        foreach (var dependency in dependencies)
            CurrentNode.AddDependency(dependency);
        return this;
    }

    public IServiceProviderBuilder AddTransient<T>() where T : class
    {
        var type = typeof(T);
        EnsureNoMultiRegister(type);
        CurrentNode = new TransientCtorNode(type, type);
        FigureOutDependenciesFromCtor();
        return this;
    }

    IServiceProviderBuilder IServiceProviderBuilder.AddTransient<TAs, TInstance>()
    {
        var type = typeof(TAs);
        EnsureNoMultiRegister(typeof(TAs));
        CurrentNode = new TransientCtorNode(typeof(TInstance),type);
        FigureOutDependenciesFromCtor();
        return this;
    }

}
