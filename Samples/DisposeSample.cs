using Samples.Net;

using System;
using System.Collections.Generic;
using System.Text;

namespace Samples
{
    [Sample]
    public class DisposeSample : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("DisposeSample is running.");

            Console.WriteLine("DisposeSample is disposing...");

            Console.WriteLine("DisposeSample is done.");
        }
    }
}
