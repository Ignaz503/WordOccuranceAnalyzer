using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

public partial class ServiceProviderBuilder
{
    /// <summary>
    /// Node for singleton where the object instance is provided
    /// </summary>
    /// <typeparam name="T">Type of service</typeparam>
    internal class SingletonObjectNode<T> : Node
        where T : class
    {
        T _object;

        public SingletonObjectNode(T obj) : base(typeof(T), typeof(T))
        {
            this._object = obj;
        }

        public SingletonObjectNode(T obj, Type serviceType) : base(typeof(T), serviceType)
        {
            this._object = obj;
        }

        public override IServiceInstantiator GetInstantiator(IServiceProvider provider)
            => new SingletonObjectServiceInstantiator<T>(_object);
    }

}
