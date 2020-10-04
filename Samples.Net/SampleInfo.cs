using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Information about a Sample.
    /// </summary>
    public class SampleInfo
    {

        /// <summary>
        /// The name of the sample.
        /// </summary>
        /// <remarks>
        /// Is not required to be unique.
        /// </remarks>
        public string Name;

        /// <summary>
        /// The description of the sample.
        /// </summary>
        /// <remarks>
        /// Can be null.
        /// </remarks>
        public string Description;

        /// <summary>
        /// The Type of the Sample.
        /// </summary>
        public Type Type;
    }
}
