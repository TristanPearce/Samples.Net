using Samples.Net;

using System;
using System.Collections.Generic;
using System.Text;

namespace Samples
{
    [Sample]
    public class NamedParameterSample
    {


        [Run]
        public void Foo(string apiToken, int callLimit)
        {
            Console.WriteLine($"Token: {apiToken}, limit: {callLimit}.");
        }
    }
}
