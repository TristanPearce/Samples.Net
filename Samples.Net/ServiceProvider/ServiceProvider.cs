using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    internal sealed class ServiceProvider : IServiceProvider
    {

        private IDictionary<Type, IService> services; 

        public ServiceProvider()
        {
            services = new Dictionary<Type, IService>();
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
