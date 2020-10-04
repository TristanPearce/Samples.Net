using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Text used to guide a user in working with the sample.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HelpAttribute : Attribute
    {
        public string Text { get; private set; }

        public HelpAttribute(string text)
        {
            this.Text = text;
        }

    }
}
