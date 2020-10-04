using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Provides a name to a sample.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NameAttribute : Attribute
    {

        public readonly string Text;

        public NameAttribute(string name)
        {
            this.Text = name;
        }
    }
}
