using TextFileContentAnalyzer.Core.ServiceProvider.Instantiators;

namespace TextFileContentAnalyzer.Core.ServiceProvider;

public partial class ServiceProviderBuilder
{
    /// <summary>
    /// represents a service that is being added to the service provider.
    /// </summary>
    internal abstract class Node
    {
        public abstract IServiceInstantiator GetInstantiator(IServiceProvider provider);

        public Type ServiceType { get; private set; }
        public Type ImplementorType { get; private set; }
        protected List<Type> Dependencies { get; private set; }

        public Node(Type implementorType, Type serviceType)
        {
            this.ImplementorType = implementorType ?? throw new ArgumentNullException(nameof(implementorType));
            this.ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            Dependencies = new();
        }

        /// <summary>
        /// Adds a dependency on another service.
        /// </summary>
        /// <param name="t">Type of dependency.</param>
        public void AddDependency(Type t)
        {
            Dependencies.Add(t);
        }
        /// <summary>
        /// Adds a dependency on another service.
        /// </summary>
        /// <typeparam name="T">Type of dependency.</typeparam>
        public void AddDependency<T>()
            => AddDependency(typeof(T));

        /// <summary>
        /// Validates this node with in the context of the provided builder.
        /// </summary>
        /// <param name="builder">The builder the node is part of.</param>
        /// <returns>true if node is valid false if node is invalid.</returns>
        public bool Validate(ServiceProviderBuilder builder)
        {
            var isValid = true;
            foreach (var dependencyType in Dependencies)
            {
                var dependencyNode = builder.GetNode(dependencyType);

                if (dependencyNode is null) 
                {
                    isValid = false;
                    break;
                }

                isValid &= ValidateNode(builder, dependencyNode, ImplementorType);

                if (!isValid)
                    break;
            }
            return isValid;
        }

        /// <summary>
        /// Recursive search for own type on all dependencies and their dependencies for a certain type.
        /// </summary>
        /// <param name="b">the service provider builder as context.</param>
        /// <param name="n">node to search in.</param>
        /// <param name="toLookOutFor">then type to look out for that would indicate a circular dependency.</param>
        /// <returns>true if not <paramref name="toLookOutFor"/> found false otherwise.</returns>
        bool ValidateNode(ServiceProviderBuilder b, Node n, Type toLookOutFor)
        {
            if (n.Dependencies.Count == 0)//we are at a service that depends on no other service 
                return true;
            if (n.Dependencies.Contains(toLookOutFor)) //this service depends on us and we depend on it, that is a no-no
                return false;
            //go deeper
            var result = true;
            foreach (var dependencyType in n.Dependencies)
            {
                var newNode = b.GetNode(dependencyType);

                if (newNode is null) 
                {
                    result = false;
                    break;
                }

                result &= ValidateNode(b, newNode, toLookOutFor);
                if (!result)//we already know that it won't work, so break out of it
                    break;
            }
            return result;
        }
    }

}
