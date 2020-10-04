using Samples.Net;

using System;
using System.Collections.Generic;
using System.Text;

namespace Samples
{
    [Sample]
    public class MultipleFunctionsSample
    {

        [Run(order: 1)]
        public void Run1()
        {
            Console.WriteLine("Run1 is running");
        }

        [Run(order: 0)]
        public void Run0(string ez)
        {
            Console.WriteLine("Run0 is running");
        }

        [Run(order: 2)]
        public void Run2()
        {
            Console.WriteLine("Run2 is running");
        }

    }
}
