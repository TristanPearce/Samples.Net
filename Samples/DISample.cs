using Samples.Net;

using System;
using System.Collections.Generic;
using System.Text;

namespace Samples
{
    [Sample]
    [Name("DependencyInjection Sample")]
    [Description("This sample shows how to utilise dependency injection.")]
    public class DISample
    {

        public class ExampleType
        {
            public string Name;
            public int Age;
        }


        public DISample(string str)
        {
            Console.WriteLine("str:" + str);
        }

        [Run]
        public void UseExampleType(ExampleType t)
        {
            Console.WriteLine($"Example Type Data: Name={t.Name}, Age={t.Age}.");
        }
    }
}
