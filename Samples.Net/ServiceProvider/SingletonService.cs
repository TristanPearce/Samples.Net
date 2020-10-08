using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    internal class SingletonService<T> : IService
    {
        private readonly T instance;

        public SingletonService(T instance)
        {
            this.instance = instance;                        
        }

        public object Get()
        {
            return instance;
        }
    }
}
