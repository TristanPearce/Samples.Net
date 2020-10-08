using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    internal class SingletonService : IService
    {
        private readonly object instance;

        public SingletonService(object instance)
        {
            this.instance = instance;                        
        }

        public object Get()
        {
            return instance;
        }
    }
}
