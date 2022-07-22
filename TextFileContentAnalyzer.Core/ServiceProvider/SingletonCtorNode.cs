using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

public partial class ServiceProviderBuilder
{
    /// <summary>
    /// node for singleton service created via Activator.CreateInstance
    /// </summary>
    internal class SingletonCtorNode : Node
    {
        public SingletonCtorNode(Type implementorType, Type serviceType) : base(implementorType, serviceType)
        {
        }

        public override IServiceInstantiator GetInstantiator(IServiceProvider provider)
            => new SingletonCtorServiceInstantiaor(ImplementorType, ServiceType, Dependencies, provider);
    }

}
