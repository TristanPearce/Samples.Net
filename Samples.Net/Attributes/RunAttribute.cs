using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Marks a function as to be run by a SampleRunner.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RunAttribute : Attribute
    {
        public int Order { get; private set; }

        public RunAttribute(int order = -1)
        {
            this.Order = order;
        }
    }
}
