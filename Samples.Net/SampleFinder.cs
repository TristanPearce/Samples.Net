using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Finds <see cref="Type"/> objects marked as samples using the <see cref="SampleAttribute"/>.
    /// </summary>
    public class SampleFinder
    {
        protected IList<SampleInfo> Samples;

        public SampleFinder()
        {
            Samples = new List<SampleInfo>();
        }

        /// <summary>
        /// Samples found by this <see cref="SampleFinder"/>.
        /// </summary>
        public IEnumerable<SampleInfo> GetSamples()
        {
            return this.Samples;
        }


        public virtual void Find(Assembly assembly)
        {
            Type[] allTypes = assembly.GetTypes();
            this.Find(allTypes);
        }

        public virtual void Find(params Type[] types)
        {
            foreach (Type t in types)
            {
                if ((t.GetCustomAttribute<SampleAttribute>() is SampleAttribute a))
                {
                    SampleInfo info = new SampleInfo()
                    {
                        Name = this.GetName(t),
                        Description = this.GetDescription(t),
                        Type = t
                    };

                    Samples.Add(info);
                }
            }
        }

        /// <summary>
        /// Get the name of the sample.
        /// </summary>
        /// <param name="t">Type of the sample.</param>
        /// <returns>A string containing the name of the sample.</returns>
        protected virtual string GetName(Type t)
        {
            // Samples.Net Name Attribute 
            {
                if (t.GetCustomAttribute<Samples.Net.NameAttribute>()
                        is Samples.Net.NameAttribute name)
                {
                    return name.Text;
                }
            }

            return t.Name;
        }

        /// <summary>
        /// Find the description for the sample.
        /// </summary>
        /// <param name="t">Type of the sample</param>
        /// <returns>A string containing the description of the sample. Can be <see cref="null"/>.</returns>
        protected virtual string GetDescription(Type t)
        {
            // Samples.Net Description Attribute
            {
                if (t.GetCustomAttribute<Samples.Net.DescriptionAttribute>()
                    is Samples.Net.DescriptionAttribute description)
                {
                    return description.Text;
                }
            }
            
            // System.ComponentModel Description Attribute
            {
                if (t.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>()
                    is System.ComponentModel.DescriptionAttribute description)
                {
                    return description.Description;
                }
            }

            return null;
        }
    }
}
