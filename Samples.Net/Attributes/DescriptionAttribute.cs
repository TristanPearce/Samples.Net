using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Defines a description for a sample
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DescriptionAttribute : Attribute
    { 
        public string Text { get; private set; }

        public DescriptionAttribute(string text)
        {
            this.Text = text;
        }
    }
}
