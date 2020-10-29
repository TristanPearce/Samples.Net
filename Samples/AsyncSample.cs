using Samples.Net;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    [Sample]
    public class AsyncSample
    {

        [Run]
        public async Task ExampleAsync()
        {
            Console.WriteLine("ExampleAsync: 0 seconds");
            await Task.Delay(1000);
            Console.WriteLine("ExampleAsync: 1 seconds");
            await Task.Delay(1000);
            Console.WriteLine("ExampleAsync: 2 seconds");
            await Task.Delay(1000);
            Console.WriteLine("ExampleAsync: Finished");
        }

        [Run]
        public async Task<object> ReturningAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Returning a value from async method...");
            return "This is a returned string";
        }

    }
}
