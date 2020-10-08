using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    public sealed class ServiceProvider : IServiceProvider
    {

        private IDictionary<Type, IService> services; 

        public ServiceProvider()
        {
            services = new Dictionary<Type, IService>();
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
