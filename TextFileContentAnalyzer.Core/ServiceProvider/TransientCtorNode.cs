using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

public partial class ServiceProviderBuilder
{
    /// <summary>
    /// Node for transient service created with Activator.CreateInstance
    /// </summary>
    internal class TransientCtorNode : Node 
    {
        public TransientCtorNode(Type implementorType, Type serviceType) : base(implementorType, serviceType)
        {
        }

        public override IServiceInstantiator GetInstantiator(IServiceProvider provider)
            => new CtorServiceInstantiator(ImplementorType, ServiceType, Dependencies, provider);
    }

}
