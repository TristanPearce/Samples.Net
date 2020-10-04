using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Marks a class as a sample. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SampleAttribute : Attribute
    {
        
    }
}
