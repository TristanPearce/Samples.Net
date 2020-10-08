using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    public sealed class ServiceProvider : IServiceProvider
    {

        private readonly IDictionary<Type, IService> services; 

        public ServiceProvider()
        {
            services = new Dictionary<Type, IService>();
        }

        public void AddTransient<T>(Func<T> factory)
        {
            services.Add(typeof(T), new TransientService<T>(factory));
        }

        public void AddTransient(Type type, Func<object> factory)
        {
            services.Add(type, new TransientService(factory));
        }

        public void AddSingleton<T>(T service)
        {
            services.Add(typeof(T), new SingletonService(service));
        }

        public void AddSingleton(object service)
        {
            services.Add(service.GetType(), new SingletonService(service));
        }

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
