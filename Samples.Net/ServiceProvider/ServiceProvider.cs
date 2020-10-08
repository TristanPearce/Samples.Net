using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{

    /// <summary>
    /// Default Samples.Net implementation of IServiceProvider.
    /// </summary>
    public sealed class ServiceProvider : IServiceProvider
    {

        private readonly IDictionary<Type, IService> services; 

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ServiceProvider()
        {
            services = new Dictionary<Type, IService>();
        }

        /// <summary>
        /// Add a service that produces a new object for every request.
        /// </summary>
        /// <typeparam name="T">The Type of the object to be produced.</typeparam>
        /// <param name="factory">Function used to construct the object on every request.</param>
        public void AddTransient<T>(Func<T> factory)
        {
            services.Add(typeof(T), new TransientService<T>(factory));
        }

        /// <summary>
        /// Add a service that produces the same object for every request.
        /// </summary>
        /// <typeparam name="T">The Type of the object to be produced.</typeparam>
        /// <param name="service">The instance to be returned on each request.</param>
        public void AddSingleton<T>(T service)
        {
            services.Add(typeof(T), new SingletonService<T>(service));
        }

        /// <summary>
        /// Produce an object of the specified type if available.
        /// </summary>
        /// <param name="serviceType">The Type of object to be returned.</param>
        /// <returns>Null if the requested type can't be produced.</returns>
        public object GetService(Type serviceType)
        {
            if(services.TryGetValue(serviceType, out IService service))
            {
                return service.Get();
            }

            return null;
        }
    }
}
