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
        public void Login(string token)
        {
            Console.WriteLine($"Login token: {token}");
        }


    }
}
