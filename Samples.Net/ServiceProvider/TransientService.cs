using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    internal sealed class TransientService : IService
    {
        private Func<object> factory;

        public TransientService(Func<object> factory)
        {
            this.factory = factory;
        }

        public object Get()
        {
            return factory();
        }
    }

    internal sealed class TransientService<T> : IService
    {
        private Func<T> factory;

        public TransientService(Func<T> factory)
        {
            this.factory = factory;
        }

        public object Get()
        {
            return factory();
        }
    }
}
