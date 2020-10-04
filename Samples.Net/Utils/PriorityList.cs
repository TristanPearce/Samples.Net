using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// VERY basic implementation of a PriorityList, shouldn't really be used by anyone else right now.
    /// </summary>
    /// <typeparam name="Priority">Type used as the priority, need to have an implemention of IComparable.</typeparam>
    /// <typeparam name="Value">The type to be stored in the list.</typeparam>
    public class PriorityList<Priority, Value> : IEnumerable<Value> where Priority : IComparable<Priority>
    {
        
        /// <summary>
        /// Used to store entries in the PriorityList.
        /// </summary>
        private class PriorityListEntry : IComparable<PriorityListEntry>, IComparable
        {
            public Value Data;
            public Priority Priority;

            public PriorityListEntry(Priority priority, Value data)
            {
                this.Priority = priority;
                this.Data = data;
            }

            public int CompareTo(PriorityListEntry other)
            {
                return this.Priority.CompareTo(other.Priority);
            }

            public int CompareTo(object obj)
            {
                if (obj is Priority other)
                {
                    return Priority.CompareTo(other);
                }
                throw new ArgumentException($"Object is not of type {typeof(Priority)}");
            }
        }

        private readonly List<PriorityListEntry> data;
        
        public PriorityList()
        {
            data = new List<PriorityListEntry>();
        }

        /// <summary>
        /// Add item to list at a given priority.
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="value"></param>
        public void Add(Priority priority, Value value)
        {
            data.Add(new PriorityListEntry(priority, value));
            this.Sort();
        }

        /// <summary>
        /// Get an item from the PriorityList at the given index.
        /// </summary>
        /// <param name="index">Index of item to be returned.</param>
        /// <returns>Item at the given index.</returns>
        public Value Get(int index)
        {
            return data[index].Data;
        }

        /// <summary>
        /// Sort underlying data.
        /// </summary>
        private void Sort()
        {
            data.Sort();
        }

        public IEnumerator<Value> GetEnumerator()
        {
            return data.Select(x => x.Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
