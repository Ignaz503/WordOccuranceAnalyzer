using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

public partial class ServiceProviderBuilder
{
    /// <summary>
    /// Node that represents a transient service created via factory
    /// </summary>
    /// <typeparam name="T">type of service</typeparam>
    internal class TransientFactoryNode<T> : Node
        where T : class
    {
        readonly Func<IServiceProvider, T> _factory;

        public TransientFactoryNode(Func<IServiceProvider, T> factory, Type implementorType, Type serviceType) : base(implementorType, serviceType)
        {
            this._factory = factory;
        }

        public override IServiceInstantiator GetInstantiator(IServiceProvider provider) 
            => new FactoryServiceInstantiator<T>(provider, _factory);
    }

}
