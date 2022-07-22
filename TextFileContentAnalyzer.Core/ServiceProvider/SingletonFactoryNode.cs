using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

public partial class ServiceProviderBuilder
{
    /// <summary>
    /// Node for a singleton services created with factory function.
    /// </summary>
    /// <typeparam name="T">Type of service</typeparam>
    internal class SingletonFactoryNode<T> : Node
        where T : class
    {
        readonly Func<IServiceProvider, T> _factory;

        public SingletonFactoryNode(Func<IServiceProvider, T> factory, Type implementorType, Type serviceType) : base(implementorType, serviceType)
        {
            this._factory = factory;
        }

        public override IServiceInstantiator GetInstantiator(IServiceProvider provider) 
            => new SingletonFactoryServiceInstantiator<T>(provider, _factory);
    }

}
